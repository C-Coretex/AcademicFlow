using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Domain.Contracts.Models;
using AcademicFlow.Domain.Contracts.Models.Configs;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Contracts.Models.AssignmentModels.InputModels;
using AcademicFlow.Managers.Contracts.Models.AssignmentModels.OutputModels;
using AutoMapper;
using Azure.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;

namespace AcademicFlow.Managers.Managers
{
    public class AssignmentManager : BaseManager, IAssignmentManager
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }

        private readonly IAssignmentTaskService _assignmentTaskService;
        private readonly IAssignmentEntryService _assignmentEntryService;
        private readonly IAssignmentGradeService _assignmentGradeService;
        private readonly IUserRoleService _userRoleService;
        private readonly IUserService _userService;
        private readonly IOptions<AppConfigs> _appConfigs;

        public AssignmentManager(IAssignmentTaskService assignmentTaskService, IAssignmentEntryService assignmentEntryService, 
                                 IAssignmentGradeService assignmentGradeService, IUserRoleService userRoleService, IUserService userService, IMapper mapper,
                                 IOptions<AppConfigs> appConfigs) : base(mapper)
        {
            _assignmentTaskService = assignmentTaskService;
            _assignmentEntryService = assignmentEntryService;
            _assignmentGradeService = assignmentGradeService;
            _userRoleService = userRoleService;
            _userService = userService;
            _appConfigs = appConfigs;
        }

        public async Task AddAssignmentTask(AssignmentTaskInputModel assignmentTask)
        {
            var userRole = await RoleOfUserForCourse(assignmentTask.CourseId);
            if (userRole != RolesEnum.Professor)
                throw new Exception("User is not assigned as Professor for this course");

            var assignmentTaskEntity = Mapper.Map<AssignmentTask>(assignmentTask);
            assignmentTaskEntity.CreatedById = UserId;

            await _assignmentTaskService.Add(assignmentTaskEntity);
        }

        public async Task DeleteAssignmentTask(int id)
        {
            var assignmentTask = await _assignmentTaskService.GetByIdFull(id) ?? throw new Exception("AssignmentTask is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentTask.CourseId);
            if (userRole != RolesEnum.Professor)
                throw new Exception("User is not assigned as Professor for this course");

            foreach(var assignmentEntry in assignmentTask.AssignmentEntries)
            {
                Directory.Delete(new FileInfo(assignmentEntry.AssignmentFilePath).Directory!.FullName, true);
            }

            await _assignmentTaskService.Delete(id);
        }

        public async Task<AssignmentTaskOutputModel> GetAssignmentTask(int id)
        {
            var assignmentTask = await _assignmentTaskService.GetById(id) ?? throw new Exception("AssignmentTask is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentTask.CourseId);
            if (userRole != RolesEnum.Professor && userRole != RolesEnum.Student)
                throw new Exception("User is not assigned as Professor or Student for this course");

            var returnData = Mapper.Map<AssignmentTaskOutputModel>(assignmentTask);
            return returnData;
        }

        public async Task AddAssignmentEntry(int assignmentTaskId, FileModel file)
        {
            var assignmentTask = await _assignmentTaskService.GetById(assignmentTaskId) ?? throw new Exception("AssignmentTask is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentTask.CourseId);
            if (userRole != RolesEnum.Student)
                throw new Exception("User is not assigned as Student for this course");

            var filePath = Path.Combine(_appConfigs.Value.AssignmentsFilePath, Guid.NewGuid().ToString());
            Directory.CreateDirectory(filePath);
            filePath = Path.Combine(filePath, file.FileName);
            await File.WriteAllBytesAsync(filePath, file.Data);

            var assignmentEntry = new AssignmentEntry()
            {
                CreatedById = UserId,
                AssignmentTaskId = assignmentTaskId,
                AssignmentFilePath = filePath,
            };
            await _assignmentEntryService.Add(assignmentEntry);
        }

        public async Task DeleteAssignmentEntry(int id)
        {
            var assignmentEntry = await _assignmentEntryService.GetById(id) ?? throw new Exception("AssignmentEntry is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentEntry.AssignmentTask.CourseId);
            if (userRole != RolesEnum.Student)
                throw new Exception("User is not assigned as Student for this course");

            if(assignmentEntry.AssignmentGrade != null)
                throw new Exception("Assignment is already graded");

            if (assignmentEntry.AssignmentTask.Deadline < DateTime.Now)
                throw new Exception("Assignment deadline is passed");

            Directory.Delete(new FileInfo(assignmentEntry.AssignmentFilePath).Directory!.FullName, true);
            await _assignmentEntryService.Delete(id);
        }

        public async Task<AssignmentEntryOutputModel> GetAssignmentEntry(int id)
        {
            var assignmentEntry = await _assignmentEntryService.GetById(id) ?? throw new Exception("AssignmentEntry is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentEntry.AssignmentTask.CourseId);
            if (userRole != RolesEnum.Professor && userRole != RolesEnum.Student)
                throw new Exception("User is not assigned as Professor or Student for this course");

            var returnData = Mapper.Map<AssignmentEntryOutputModel>(assignmentEntry);
            return returnData;
        }

        public async Task<FileModel> DownloadAssignmentFile(int id)
        {
            var assignmentEntry = await _assignmentEntryService.GetById(id) ?? throw new Exception("AssignmentEntry is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentEntry.AssignmentTask.CourseId);
            if (userRole != RolesEnum.Professor && userRole != RolesEnum.Student)
                throw new Exception("User is not assigned as Professor or Student for this course");

            var fileData = await File.ReadAllBytesAsync(assignmentEntry.AssignmentFilePath);
            new FileExtensionContentTypeProvider().TryGetContentType(assignmentEntry.AssignmentFilePath, out var contentType);
            var fileModel = new FileModel()
            {
                Data = fileData,
                FileName = Path.GetFileName(assignmentEntry.AssignmentFilePath),
                ContentType = contentType
            };
            return fileModel;
        }

        public async Task AddAssignmentGrade(AssignmentGradeInputModel assignmentGrade)
        {
            var assignmentEntry = await _assignmentEntryService.GetById(assignmentGrade.AssignmentEntryId) ?? throw new Exception("AssignmentEntry is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentEntry.AssignmentTask.CourseId);
            if (userRole != RolesEnum.Professor)
                throw new Exception("User is not assigned as Professor for this course");

            var assignmentGradeEntity = Mapper.Map<AssignmentGrade>(assignmentGrade);
            assignmentGradeEntity.GradedById = UserId;
            await _assignmentGradeService.Add(assignmentGradeEntity);
        }

        public async Task DeleteAssignmentGrade(int id)
        {
            var assignmentGrade = await _assignmentGradeService.GetById(id) ?? throw new Exception("AssignmentGrade is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentGrade.AssignmentEntry.AssignmentTask.CourseId);
            if (userRole != RolesEnum.Professor)
                throw new Exception("User is not assigned as Professor for this course");

            await _assignmentGradeService.Delete(id);
        }

        public async Task<AssignmentGradeOutputModel> GetAssignmentGrade(int id)
        {
            var assignmentGrade = await _assignmentGradeService.GetById(id) ?? throw new Exception("AssignmentGrade is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentGrade.AssignmentEntry.AssignmentTask.CourseId);
            if (userRole != RolesEnum.Professor && userRole != RolesEnum.Student)
                throw new Exception("User is not assigned as Professor or Student for this course");

            var returnData = Mapper.Map<AssignmentGradeOutputModel>(assignmentGrade);
            return returnData;
        }

        public async Task<AssignmentsOutputModel> GetAssignmentEntriesForAssignmentTask(int id)
        {
            var assignmentTask = await _assignmentTaskService.GetByIdFull(id) ?? throw new Exception("AssignmentTask is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentTask.CourseId);
            if (userRole != RolesEnum.Professor)
                throw new Exception("User is not assigned as Professor for this course");

            var returnData = new AssignmentsOutputModel(assignmentTask, Mapper);
            return returnData;
        }

        public async Task<AssignmentsOutputModel> GetMyAssignmentEntryForAssignmentTask(int id)
        {
            var assignmentTask = await _assignmentTaskService.GetByIdFull(id) ?? throw new Exception("AssignmentTask is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentTask.CourseId);
            if (userRole != RolesEnum.Student)
                throw new Exception("User is not assigned as Student for this course");

            assignmentTask.AssignmentEntries = assignmentTask.AssignmentEntries.Where(x => x.CreatedById == UserId).ToList();
            var returnData = new AssignmentsOutputModel(assignmentTask, Mapper);
            return returnData;
        }

        public async Task<IEnumerable<AssignmentsOutputModel>> GetAllAssignmentsForCourse(int courseId, bool withAssignedEntries, bool withGrades, DateTime? dateFrom, DateTime? dateTo)
        {
             var userRole = await RoleOfUserForCourse(courseId);
            if (userRole != RolesEnum.Student && userRole != RolesEnum.Professor)
                throw new Exception("User is not assigned as Student or Professor for this course");

            var assignmentTasks =  _assignmentTaskService.GetByCourseId(courseId);
            if (userRole == RolesEnum.Student)
                assignmentTasks = _assignmentTaskService.IncludeAssignmentEntriesWithGrades(assignmentTasks, UserId);
            else
                assignmentTasks = _assignmentTaskService.IncludeAssignmentEntriesWithGrades(assignmentTasks);

            if (!withAssignedEntries)
                assignmentTasks = assignmentTasks.Where(x => x.AssignmentEntries == null);
            if (dateFrom != null)
                assignmentTasks = assignmentTasks.Where(x => x.Modified >= dateFrom);
            if (dateTo != null)
                assignmentTasks = assignmentTasks.Where(x => dateTo >= x.Modified);
            if (!withGrades)
            {
                assignmentTasks = assignmentTasks.ToList().AsQueryable();
                foreach (var assignmentTask in assignmentTasks.Where(x => x.AssignmentEntries != null && x.AssignmentEntries.Count > 0))
                    assignmentTask.AssignmentEntries = assignmentTask.AssignmentEntries.Where(x => x.AssignmentGrade != null).ToList();
            }

            var returnData = assignmentTasks.Select(x => new AssignmentsOutputModel(x, Mapper)).ToList();
            return returnData;
        }

        public async Task<IEnumerable<AssignmentsOutputModelByCourse>> GetAllAssignmentsForAllCourses()
        {
            var user = await _userService.GetUserByIdWithAssignments(UserId) ?? throw new Exception("User by this id is not found"); ;                

            var courseIdAssignmentTaskGroup = user.AssignmentTasks.GroupBy(x => x.CourseId);
            var returnData = courseIdAssignmentTaskGroup.Select(x => new AssignmentsOutputModelByCourse(x.First().Course, Mapper)).ToList();

            return returnData;
        }

        private Task<RolesEnum> RoleOfUserForCourse(int courseId)
        {
            CourseId = courseId;
            return RoleOfUserForCourse();
        }

        private async Task<RolesEnum> RoleOfUserForCourse()
        {
            var userRole = await _userRoleService.GetByUserId(UserId, CourseId) ?? throw new Exception("UserRole is not found by this userId and courseID");

            return userRole.Role;
        }
    }
}

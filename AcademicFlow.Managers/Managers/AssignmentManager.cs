using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Domain.Contracts.Models;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Contracts.Models.AssignmentModels.InputModels;
using AcademicFlow.Managers.Contracts.Models.AssignmentModels.OutputModels;

namespace AcademicFlow.Managers.Managers
{
    public class AssignmentManager : IAssignmentManager
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }

        private readonly IAssignmentTaskService _assignmentTaskService;
        private readonly IAssignmentEntryService _assignmentEntryService;
        private readonly IAssignmentGradeService _assignmentGradeService;
        private readonly IUserRoleService _userRoleService;
        private readonly IUserService _userService;

        public AssignmentManager(IAssignmentTaskService assignmentTaskService, IAssignmentEntryService assignmentEntryService, 
                                 IAssignmentGradeService assignmentGradeService, IUserRoleService userRoleService, IUserService userService)
        {
            _assignmentTaskService = assignmentTaskService;
            _assignmentEntryService = assignmentEntryService;
            _assignmentGradeService = assignmentGradeService;
            _userRoleService = userRoleService;
            _userService = userService;
        }

        public async Task AddAssignmentTask(AssignmentTaskInputModel assignmentTask)
        {
            var userRole = await RoleOfUserForCourse(assignmentTask.CourseID);
            if (userRole != RolesEnum.Professor)
                throw new Exception("User is not assigned as Professor for this course");
        }

        public async Task DeleteAssignmentTask(int id)
        {
            var assignmentTask = await _assignmentTaskService.GetById(id) ?? throw new Exception("AssignmentTask is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentTask.CourseId);
            if (userRole != RolesEnum.Professor)
                throw new Exception("User is not assigned as Professor for this course");
        }

        public async Task<AssignmentTaskOutputModel> GetAssignmentTask(int id)
        {
            var assignmentTask = await _assignmentTaskService.GetById(id) ?? throw new Exception("AssignmentTask is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentTask.CourseId);
            if (userRole != RolesEnum.Professor && userRole != RolesEnum.Student)
                throw new Exception("User is not assigned as Professor or Student for this course");

            return null;
        }

        public async Task AddAssignmentEntry(int assignmentTaskId, FileModel file)
        {
            var assignmentTask = await _assignmentTaskService.GetById(assignmentTaskId) ?? throw new Exception("AssignmentTask is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentTask.CourseId);
            if (userRole != RolesEnum.Student)
                throw new Exception("User is not assigned as Student for this course");
        }

        public async Task DeleteAssignmentEntry(int id)
        {
            var assignmentEntry = await _assignmentEntryService.GetById(id) ?? throw new Exception("AssignmentEntry is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentEntry.AssignmentTask.CourseId);
            if (userRole != RolesEnum.Student)
                throw new Exception("User is not assigned as Student for this course");
        }

        public async Task<AssignmentEntryOutputModel> GetAssignmentEntry(int id)
        {
            var assignmentEntry = await _assignmentEntryService.GetById(id) ?? throw new Exception("AssignmentEntry is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentEntry.AssignmentTask.CourseId);
            if (userRole != RolesEnum.Professor && userRole != RolesEnum.Student)
                throw new Exception("User is not assigned as Professor or Student for this course");

            return null;
        }

        public async Task<FileModel> DownloadAssignmentFile(int id)
        {
            var assignmentEntry = await _assignmentEntryService.GetById(id) ?? throw new Exception("AssignmentEntry is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentEntry.AssignmentTask.CourseId);
            if (userRole != RolesEnum.Professor && userRole != RolesEnum.Student)
                throw new Exception("User is not assigned as Professor or Student for this course");

            return null;
        }

        public async Task AddAssignmentGrade(AssignmentGradeInputModel assignmentGrade)
        {
            var assignmentEntry = await _assignmentEntryService.GetById(assignmentGrade.AssignmentEntryId) ?? throw new Exception("AssignmentEntry is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentEntry.AssignmentTask.CourseId);
            if (userRole != RolesEnum.Professor)
                throw new Exception("User is not assigned as Professor for this course");
        }

        public async Task DeleteAssignmentGrade(int id)
        {
            var assignmentGrade = await _assignmentGradeService.GetById(id) ?? throw new Exception("AssignmentGrade is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentGrade.AssignmentEntry.AssignmentTask.CourseId);
            if (userRole != RolesEnum.Professor)
                throw new Exception("User is not assigned as Professor for this course");
        }

        public async Task<AssignmentGradeOutputModel> GetAssignmentGrade(int id)
        {
            var assignmentGrade = await _assignmentGradeService.GetById(id) ?? throw new Exception("AssignmentGrade is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentGrade.AssignmentEntry.AssignmentTask.CourseId);
            if (userRole != RolesEnum.Professor && userRole != RolesEnum.Student)
                throw new Exception("User is not assigned as Professor or Student for this course");

            return null;
        }

        public async Task<AssignmentsOutputModel> GetAssignmentEntriesForAssignmentTask(int id)
        {
            var assignmentGrade = await _assignmentTaskService.GetByIdFull(id) ?? throw new Exception("AssignmentTask is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentGrade.CourseId);
            if (userRole != RolesEnum.Professor)
                throw new Exception("User is not assigned as Professor for this course");

            return null;
        }

        public async Task<AssignmentsOutputModel> GetMyAssignmentEntryForAssignmentTask(int id)
        {
            var assignmentGrade = await _assignmentTaskService.GetByIdFull(id) ?? throw new Exception("AssignmentTask is not found by this Id");
            var userRole = await RoleOfUserForCourse(assignmentGrade.CourseId);
            if (userRole != RolesEnum.Student)
                throw new Exception("User is not assigned as Student for this course");

            return null;
        }

        public async Task<AssignmentsOutputModel> GetAllAssignmentsForCourse(int courseId, bool withAssignedEntries, bool withGrades, DateTime? dateFrom, DateTime? dateTo)
        {
            var userRole = await RoleOfUserForCourse(courseId);
            if (userRole != RolesEnum.Student && userRole != RolesEnum.Professor)
                throw new Exception("User is not assigned as Student or Professor for this course");

            return null;
        }

        public async Task<AssignmentsOutputModel> GetAllAssignmentGradesForCourse(int id)
        {
            var userRole = await RoleOfUserForCourse(id);
            if (userRole != RolesEnum.Student)
                throw new Exception("User is not assigned as Student for this course");

            return null;
        }

        public async Task<AssignmentsOutputModel> GetAllAssignmentGradesForAllCourses()
        {
            var user = await _userService.GetUserByIdWithAssignments(UserId);

            return null;
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

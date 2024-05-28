using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Domain.Contracts.Models;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Contracts.Models.AssignmentModels.InputModels;
using AcademicFlow.Managers.Contracts.Models.AssignmentModels.OutputModels;

namespace AcademicFlow.Managers.Managers
{
    public class AssignmentManager: IAssignmentManager
    {
        public int UserId { get; set; }

        private readonly IAssignmentTaskService _assignmentTaskService;
        private readonly IAssignmentEntryService _assignmentEntryService;
        private readonly IAssignmentGradeService _assignmentGradeService;
        public AssignmentManager(IAssignmentTaskService assignmentTaskService, IAssignmentEntryService assignmentEntryService, IAssignmentGradeService assignmentGradeService)
        {
            _assignmentTaskService = assignmentTaskService;
            _assignmentEntryService = assignmentEntryService;
            _assignmentGradeService = assignmentGradeService;
        }

        public Task AddAssignmentTask(AssignmentTaskInputModel assignmentTask)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAssignmentTask(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AssignmentTaskOutputModel> GetAssignmentTask(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAssignmentEntry(int assignmentTaskId, FileModel file)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAssignmentEntry(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AssignmentEntryOutputModel> GetAssignmentEntry(int id)
        {
            throw new NotImplementedException();
        }

        public Task<FileModel> DownloadAssignmentFile(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAssignmentGrade(AssignmentGradeInputModel assignmentGrade)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAssignmentGrade(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AssignmentGradeOutputModel> GetAssignmentGrade(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AssignmentsOutputModel> GetAssignmentEntriesForAssignmentTask(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AssignmentsOutputModel> GetMyAssignmentEntryForAssignmentTask(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AssignmentsOutputModel> GetAllAssignmentsForCourse(bool withAssignedEntries, bool withGrades, DateTime? dateFrom, DateTime? dateTo)
        {
            throw new NotImplementedException();
        }

        public Task<AssignmentsOutputModel> GetAllAssignmentGradesForCourse(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AssignmentsOutputModel> GetAllAssignmentGradesForAllCourses()
        {
            throw new NotImplementedException();
        }
    }
}

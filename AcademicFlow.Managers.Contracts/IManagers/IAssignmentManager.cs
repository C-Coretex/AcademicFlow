using AcademicFlow.Domain.Contracts.Models;
using AcademicFlow.Managers.Contracts.Models.AssignmentModels.InputModels;
using AcademicFlow.Managers.Contracts.Models.AssignmentModels.OutputModels;

namespace AcademicFlow.Managers.Contracts.IManagers
{
    public interface IAssignmentManager
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }

        Task AddAssignmentTask(AssignmentTaskInputModel assignmentTask);
        Task DeleteAssignmentTask(int id);
        Task<AssignmentTaskOutputModel> GetAssignmentTask(int id);
        Task AddAssignmentEntry(int assignmentTaskId, FileModel file);
        Task DeleteAssignmentEntry(int id);
        Task<AssignmentEntryOutputModel> GetAssignmentEntry(int id);
        Task<FileModel> DownloadAssignmentFile(int id);
        Task AddAssignmentGrade(AssignmentGradeInputModel assignmentGrade);
        Task DeleteAssignmentGrade(int id);
        Task<AssignmentGradeOutputModel> GetAssignmentGrade(int id);
        Task<AssignmentsOutputModel> GetAssignmentEntriesForAssignmentTask(int id);
        Task<AssignmentsOutputModel> GetMyAssignmentEntryForAssignmentTask(int id);
        Task<AssignmentsOutputModel> GetAllAssignmentsForCourse(int courseId, bool withAssignedEntries, bool withGrades, DateTime? dateFrom, DateTime? dateTo);
        Task<AssignmentsOutputModel> GetAllAssignmentGradesForCourse(int id);
        Task<AssignmentsOutputModel> GetAllAssignmentGradesForAllCourses();
    }
}

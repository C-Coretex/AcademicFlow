using AcademicFlow.Domain.Contracts.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface IAssignmentGradeService
    {
        Task Add(AssignmentGrade assignmentGrade);
        Task Delete(int id);
        Task<AssignmentGrade?> GetById(int id);
    }
}

using AcademicFlow.Domain.Contracts.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface IAssignmentGradeService
    {
        Task<AssignmentGrade?> GetById(int id);
    }
}

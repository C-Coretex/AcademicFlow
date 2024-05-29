using AcademicFlow.Domain.Contracts.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface IAssignmentTaskService
    {
        Task<AssignmentTask?> GetById(int id);
        Task<AssignmentTask?> GetByIdFull(int id);
    }
}

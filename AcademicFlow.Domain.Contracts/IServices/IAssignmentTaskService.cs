using AcademicFlow.Domain.Contracts.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface IAssignmentTaskService
    {
        Task Add(AssignmentTask assignmentTask);
        Task Delete(int id);
        Task<AssignmentTask?> GetById(int id);
        Task<AssignmentTask?> GetByIdFull(int id, bool asNoTracking = true);
    }
}

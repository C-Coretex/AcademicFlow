using AcademicFlow.Domain.Contracts.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface IAssignmentEntryService
    {
        Task Add(AssignmentEntry assignmentEntry);
        Task Delete(int id);
        Task<AssignmentEntry?> GetById(int id);
    }
}

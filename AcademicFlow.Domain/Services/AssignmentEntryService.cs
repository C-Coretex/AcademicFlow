using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Contracts.IServices;
using Microsoft.EntityFrameworkCore;

namespace AcademicFlow.Domain.Services
{
    public class AssignmentEntryService : IAssignmentEntryService
    {
        private readonly IAssignmentEntryRepository _assignmentEntryRepository;

        public AssignmentEntryService(IAssignmentEntryRepository assignmentEntryRepository)
        {
            _assignmentEntryRepository = assignmentEntryRepository;
        }

        public Task Add(AssignmentEntry assignmentEntry)
        {
            assignmentEntry.Modified = DateTime.Now;
            return _assignmentEntryRepository.AddAsync(assignmentEntry);
        }

        public Task Delete(int id)
        {
            return _assignmentEntryRepository.DeleteAsync(id);
        }

        public Task<AssignmentEntry?> GetById(int id)
        {
            return _assignmentEntryRepository.GetAll().Include(x => x.AssignmentTask).Include(x => x.AssignmentGrade).Include(x => x.CreatedById).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

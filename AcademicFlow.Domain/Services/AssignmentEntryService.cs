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

        public Task<AssignmentEntry?> GetById(int id)
        {
            return _assignmentEntryRepository.GetAll().Include(x => x.AssignmentTask).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

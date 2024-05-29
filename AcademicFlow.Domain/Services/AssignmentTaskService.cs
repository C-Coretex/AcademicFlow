using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Contracts.IServices;
using Microsoft.EntityFrameworkCore;

namespace AcademicFlow.Domain.Services
{
    public class AssignmentTaskService : IAssignmentTaskService
    {
        private readonly IAssignmentTaskRepository _assignmentTaskRepository;
        public AssignmentTaskService(IAssignmentTaskRepository assignmentTaskRepository)
        {
            _assignmentTaskRepository = assignmentTaskRepository;
        }
        public Task<AssignmentTask?> GetById(int id)
        {
            return _assignmentTaskRepository.GetByIdAsync(id);
        }

        public Task<AssignmentTask?> GetByIdFull(int id)
        {
            return _assignmentTaskRepository.GetAll().Include(x => x.AssignmentEntries).ThenInclude(x => x.Select(y => y.AssignmentGrade))
                    .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

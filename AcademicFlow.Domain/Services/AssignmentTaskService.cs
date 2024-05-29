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

        public Task Add(AssignmentTask assignmentTask)
        {
            assignmentTask.Modified = DateTime.Now;
            return _assignmentTaskRepository.AddAsync(assignmentTask);
        }

        public Task Delete(int id)
        {
            return _assignmentTaskRepository.DeleteAsync(id);
        }

        public Task<AssignmentTask?> GetById(int id)
        {
            return _assignmentTaskRepository.GetByIdAsync(id);
        }

        public Task<AssignmentTask?> GetByIdFull(int id, bool asNoTracking = true)
        {
            return _assignmentTaskRepository.GetAll(asNoTracking).Include(x => x.AssignmentEntries).ThenInclude(x => x.Select(y => y.AssignmentGrade))
                    .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

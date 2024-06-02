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
            return _assignmentTaskRepository.GetAll().Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<AssignmentTask?> GetByIdFull(int id, bool asNoTracking = true)
        {
            return _assignmentTaskRepository.GetAll(asNoTracking).Include(x => x.User).Include(x => x.AssignmentEntries).ThenInclude(x => x.User).Include(x => x.AssignmentEntries).ThenInclude(x => x.AssignmentGrade).ThenInclude(x => x.User)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<AssignmentTask> GetByCourseId(int id)
        {
            return _assignmentTaskRepository.GetAll().Where(x => x.CourseId == id);
        }

        public IQueryable<AssignmentTask> IncludeAssignmentEntriesWithGrades(IQueryable<AssignmentTask> query, int? entryUserId = null)
        {
            if(entryUserId.HasValue)
                query = query.Include(x => x.AssignmentEntries.Where(y => y.CreatedById == entryUserId)).ThenInclude(x => x.AssignmentGrade);
            else
                query = query.Include(x => x.AssignmentEntries).ThenInclude(x => x.AssignmentGrade);

            return query;
        }
    }
}

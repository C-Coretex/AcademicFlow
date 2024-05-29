using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Contracts.IServices;
using Microsoft.EntityFrameworkCore;

namespace AcademicFlow.Domain.Services
{
    public class AssignmentGradeService : IAssignmentGradeService
    {
        private readonly IAssignmentGradeRepository _assignmentGradeRepository;
        public AssignmentGradeService(IAssignmentGradeRepository assignmentGradeRepository)
        {
            _assignmentGradeRepository = assignmentGradeRepository;
        }

        public Task Add(AssignmentGrade assignmentGrade)
        {
            assignmentGrade.Modified = DateTime.Now;
            return _assignmentGradeRepository.AddAsync(assignmentGrade);
        }

        public Task Delete(int id)
        {
            return _assignmentGradeRepository.DeleteAsync(id);
        }

        public Task<AssignmentGrade?> GetById(int id)
        {
            return _assignmentGradeRepository.GetAll().Include(x => x.AssignmentEntry).ThenInclude(x => x.AssignmentTask).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

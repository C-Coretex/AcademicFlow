using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Contracts.IServices;
using Microsoft.EntityFrameworkCore;

namespace AcademicFlow.Domain.Services
{
    public class CourseProgramService(ICourseProgramRepository courseProgramRepository) : ICourseProgramService
    {
        private readonly ICourseProgramRepository _courseProgramRepository = courseProgramRepository;

        public IQueryable<CourseProgram> GetAll()
        {
            return _courseProgramRepository.GetAll().Include(x => x.Program).Include(x => x.Course).AsNoTracking();
        }

        public async Task DeleteRangeAsync(IEnumerable<CourseProgram> coursePrograms)
        {
            await _courseProgramRepository.DeleteRangeAsync(coursePrograms);
        }

        public async Task AddRangeAsync(IEnumerable<CourseProgram> coursePrograms)
        {
            await _courseProgramRepository.AddRangeAsync(coursePrograms);
        }
    }
}

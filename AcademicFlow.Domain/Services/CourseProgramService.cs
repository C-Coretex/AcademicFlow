using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Contracts.IServices;

namespace AcademicFlow.Domain.Services
{
    public class CourseProgramService(ICourseProgramRepository courseProgramRepository) : ICourseProgramService
    {
        private readonly ICourseProgramRepository _courseProgramRepository = courseProgramRepository;

        public IQueryable<CourseProgram> GetAll()
        {
            return _courseProgramRepository.GetAll();
        }

        public void DeleteRange(IEnumerable<CourseProgram> coursePrograms)
        {
            _courseProgramRepository.DeleteRange(coursePrograms);
        }

        public void AddRange(IEnumerable<CourseProgram> coursePrograms)
        {
            _courseProgramRepository.AddRange(coursePrograms);
        }
    }
}

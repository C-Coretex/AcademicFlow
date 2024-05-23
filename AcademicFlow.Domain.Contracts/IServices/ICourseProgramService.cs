using AcademicFlow.Domain.Contracts.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface ICourseProgramService
    {
        IQueryable<CourseProgram> GetAll();
        void DeleteRange(IEnumerable<CourseProgram> coursePrograms);
        void AddRange(IEnumerable<CourseProgram> coursePrograms);
    }
}

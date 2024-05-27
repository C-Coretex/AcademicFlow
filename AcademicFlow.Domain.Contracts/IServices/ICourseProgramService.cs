using AcademicFlow.Domain.Contracts.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface ICourseProgramService
    {
        IQueryable<CourseProgram> GetAll();
        Task DeleteRangeAsync(IEnumerable<CourseProgram> coursePrograms);
        Task AddRangeAsync(IEnumerable<CourseProgram> coursePrograms);
    }
}

using AcademicFlow.Domain.Contracts.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface ICourseService
    {
        Task<int?> AddCourse(Course course);

        Task<Course?> GetCourseById(int id);

        Task UpdateCourse(Course course);

        IQueryable<Course> GetAll();

        Task DeleteCourse(int id);

        Task DeleteCourseUserRolesRange(IEnumerable<CourseUserRole> userRoles);

        Task AddCourseUserRolesRange(IEnumerable<CourseUserRole> userRoles);

        IEnumerable<CourseUserRole> GetUserRoles();
    }
}

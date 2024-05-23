using AcademicFlow.Domain.Contracts.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface ICourseService
    {
        int? AddCourse(Course course);

        Course? GetCourseById(int id);

        void UpdateCourse(Course course);

        void DeleteCourse(int id);

        IQueryable<Course> GetAll();
        
        void DeleteCourseUserRolesRange(IEnumerable<CourseUserRole> userRoles);
        
        void AddCourseUserRolesRange(IEnumerable<CourseUserRole> userRoles);
    }
}

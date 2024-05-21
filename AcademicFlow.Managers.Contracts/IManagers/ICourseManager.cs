using AcademicFlow.Domain.Contracts.Entities;

namespace AcademicFlow.Managers.Contracts.IManagers
{
    public interface ICourseManager
    {
        int? AddCourse(Course course);

        Course? GetCourseById(int id);

        void UpdateCourse(Course course);
    }
}

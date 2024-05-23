using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Managers.Contracts.Models.CourseModels;

namespace AcademicFlow.Managers.Contracts.IManagers
{
    public interface ICourseManager
    {
        int? AddCourse(Course course);
        
        void DeleteCourse(int id);
        
        Course? GetCourseById(int id);

        void UpdateCourse(Course course);

        IEnumerable<CourseTableItem> GetCourseTableItemList(int? userId);

        void EditCoursePrograms(int courseId, int[] programIds);
    }
}

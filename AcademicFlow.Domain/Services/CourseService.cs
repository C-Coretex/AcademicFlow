using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Contracts.IServices;

namespace AcademicFlow.Domain.Services
{
    public class CourseService(ICourseRepository courseRepository, ICourseUserRoleRepository courseUserRoleRepository) : ICourseService
    {
        private readonly ICourseRepository _courseRepository = courseRepository;
        private readonly ICourseUserRoleRepository _courseUserRoleRepository = courseUserRoleRepository;

        public int? AddCourse(Course course)
        {
            return _courseRepository.Add(course)?.Id;
        }

        public Course? GetCourseById(int id)
        {
            return _courseRepository.GetById(id);
        }

        public void UpdateCourse(Course course)
        {
            _courseRepository.Update(course);
        }

        public IQueryable<Course> GetAll()
        {
            return _courseRepository.GetAll();
        }

        public void DeleteCourse(int id)
        {
            _courseRepository.Delete(id);
        }

        public void DeleteCourseUserRolesRange(IEnumerable<CourseUserRole> userRoles)
        {
            _courseUserRoleRepository.DeleteRange(userRoles);
        }

        public void AddCourseUserRolesRange(IEnumerable<CourseUserRole> userRoles)
        {
            _courseUserRoleRepository.AddRange(userRoles);
        }

        public IEnumerable<CourseUserRole> GetUserRoles()
        {
            return _courseUserRoleRepository.GetAll();
        }
    }
}


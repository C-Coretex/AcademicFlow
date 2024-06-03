using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Contracts.IServices;
using Microsoft.EntityFrameworkCore;

namespace AcademicFlow.Domain.Services
{
    public class CourseService(ICourseRepository courseRepository, ICourseUserRoleRepository courseUserRoleRepository) : ICourseService
    {
        private readonly ICourseRepository _courseRepository = courseRepository;
        private readonly ICourseUserRoleRepository _courseUserRoleRepository = courseUserRoleRepository;

        public async Task<int?> AddCourse(Course course)
        {
            return (await _courseRepository.AddAsync(course))?.Id;
        }

        public async Task<Course?> GetCourseById(int id)
        {
            return await _courseRepository.GetByIdAsync(id);
        }

        public async Task UpdateCourse(Course course)
        {
            await _courseRepository.UpdateAsync(course);
        }

        public IQueryable<Course> GetAll()
        {
            return _courseRepository.GetAll().Include(x => x.Programs).ThenInclude(x => x.Program).ThenInclude(x => x.UserRoles)
                .Include(x => x.Users).ThenInclude(x => x.UserRole).AsNoTracking();
        }

        public async Task DeleteCourse(int id)
        {
            await _courseRepository.DeleteAsync(id);
        }

        public async Task DeleteCourseUserRolesRange(IEnumerable<CourseUserRole> userRoles)
        {
            await _courseUserRoleRepository.DeleteRangeAsync(userRoles);
        }

        public async Task AddCourseUserRolesRange(IEnumerable<CourseUserRole> userRoles)
        {
            await _courseUserRoleRepository.AddRangeAsync(userRoles);
        }

        public IEnumerable<CourseUserRole> GetUserRoles()
        {
            return _courseUserRoleRepository.GetAll().Include(x => x.UserRole).ThenInclude(x => x.User).Include(x => x.Course);
        }

        public IQueryable<Course> GetCoursesWithAssignmentsByUserId(int userId)
        {
            return _courseRepository.GetAll(false).Include(x => x.Users.Where(x => x.Id == userId)).Include(x => x.AssignmentTasks).ThenInclude(x => x.User).ThenInclude(x => x.AssignmentEntries)
                                                                                                .ThenInclude(x => x.User).ThenInclude(x => x.AssignmentGrades).ThenInclude(x => x.User);
        }
    }
}


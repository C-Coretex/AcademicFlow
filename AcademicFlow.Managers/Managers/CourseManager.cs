using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Contracts.Models.CourseModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AcademicFlow.Managers.Managers
{
    public class CourseManager(IMapper mapper, ICourseService courseService, ICourseProgramService courseProgramService, IUserService userService)
        : BaseManager(mapper), ICourseManager
    {
        private readonly ICourseService _courseService = courseService;
        private readonly ICourseProgramService _courseProgramService = courseProgramService;
        private readonly IUserService _userService = userService;

        public async Task<int?> AddCourseAsync(Course course)
        {
            return await _courseService.AddCourse(course);
        }

        public async Task DeleteCourseAsync(int id)
        {
            await _courseService.DeleteCourse(id);
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _courseService.GetCourseById(id);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            await _courseService.UpdateCourse(course);
        }
        /// <summary>
        /// Gets Course Table;
        /// Without params - all courses;
        /// With userId and role - courses for individual the user (including by program);
        /// With program Id - courses for the program
        /// </summary>
        /// <param name="userId"> add role to get courses for individual the user (including by program)</param>
        /// <param name="assingedProgramId">get for the program</param>
        /// <param name="role">add user id to get courses for individual the user (including by program)</param>
        /// <returns></returns>
        public IEnumerable<CourseTableItem> GetCourseTableItemList(int? userId, RolesEnum? role, int? assingedProgramId)
        {
            var courses = _courseService.GetAll();
            if (userId != null && role != null)
            {
                courses = courses
                    .Where(x => (x.Users != null && x.Users.Any(y => y.UserRole.User.Id == userId && y.UserRole.Role == role))
                    || (x.Programs != null && x.Programs.Any(y => y.Program.UserRoles != null && y.Program.UserRoles.Any(z => z.UserRole.UserId == userId && z.UserRole.Role == role))));
            }
            else if (assingedProgramId != null)
            {
                courses = courses.Where(x => (x.Programs != null && x.Programs.Any(y => y.Id == assingedProgramId)));
            }
            return courses.ProjectTo<CourseTableItem>(MapperConfig).AsEnumerable();
        }

        public async Task EditCourseProgramsAsync(int courseId, int[] programIds)
        {
            var oldPrograms = _courseProgramService.GetAll().Where(x => x.CourseId == courseId).ToList();

            var toDeletePrograms = oldPrograms.Where(x => !programIds.Contains(x.ProgramId));
            await _courseProgramService.DeleteRangeAsync(toDeletePrograms);

            var oldIds = oldPrograms.Select(x => x.ProgramId);
            var toInsertPrograms = programIds.Where(x => !oldIds.Contains(x)).Select(x => new CourseProgram() { CourseId = courseId, ProgramId = x });
            await _courseProgramService.AddRangeAsync(toInsertPrograms);
        }

        public async Task EditCourseUserRoles(int courseId, int[] usersIds, RolesEnum role)
        {
            var users = _userService.GetUsers();
            var oldUsers = users
                .Where(x => x.UserRoles.Any(y => y.Courses != null && y.Courses.Any(z => z.Course.Id == courseId)))
                .ToList();

            var toDeleteCourseUsers = oldUsers
                .Where(x => !usersIds.Contains(x.Id))
                .Select(x => x.UserRoles.Where(x => x.Role == role).FirstOrDefault())
                .Where(x => x != null) 
                .Select(x => new CourseUserRole()
                {
                    CourseId = courseId,
                    UserRoleId = x.Id
                });
            await _courseService.DeleteCourseUserRolesRange(toDeleteCourseUsers);

            var toInsertCourseUsers = users
                .Where(x => usersIds.Contains(x.Id))
                .Select(x => x.UserRoles.Where(x => x.Role == role).FirstOrDefault())
                .Where(x => x != null) 
                .Select(x => new CourseUserRole()
                {
                    CourseId = courseId,
                    UserRoleId = x.Id
                });
            await _courseService.AddCourseUserRolesRange(toInsertCourseUsers);
        }

        public IEnumerable<User> GetCourseUsers(int courseId, RolesEnum role)
        {
            return _courseService
                .GetUserRoles()
                .Where(x => x.CourseId == courseId && x.UserRole.Role == role)
                .Select(x => x.UserRole.User)
                .ToList();
        }

        public IEnumerable<Program> GetCoursePrograms(int courseId)
        {
            return _courseProgramService
                .GetAll()
                .Where(x => x.CourseId == courseId)
                .Select(x => x.Program)
                .ToList();
        }
    }
}

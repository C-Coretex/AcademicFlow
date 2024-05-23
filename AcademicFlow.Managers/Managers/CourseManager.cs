using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Contracts.Models.CourseModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace AcademicFlow.Managers.Managers
{
    public class CourseManager(IMapper mapper, ICourseService courseService, ICourseProgramService courseProgramService, IUserService userService)
        : BaseManager(mapper), ICourseManager
    {
        private readonly ICourseService _courseService = courseService;
        private readonly ICourseProgramService _courseProgramService = courseProgramService;
        private readonly IUserService _userService = userService;

        public int? AddCourse(Course course)
        {
            return _courseService.AddCourse(course);
        }

        public void DeleteCourse(int id)
        {
            _courseService.DeleteCourse(id);
        }

        public Course? GetCourseById(int id)
        {
            return _courseService.GetCourseById(id);
        }

        public void UpdateCourse(Course course)
        {
            _courseService.UpdateCourse(course);
        }

        public IEnumerable<CourseTableItem> GetCourseTableItemList(int? userId)
        {
            var courses = _courseService.GetAll();
            if (userId != null)
            {
                courses = courses
                    .Where(x => (x.Users != null && x.Users.Any(x => x.UserRole.User.Id == userId))
                    || (x.Programs != null && x.Programs.Any(x => x.Program.Users != null && x.Program.Users.Any(x => x.User.UserId == userId))));
            }
            return courses.ProjectTo<CourseTableItem>(MapperConfig).AsEnumerable();
        }

        public void EditCoursePrograms(int courseId, int[] programIds)
        {
            var oldPrograms = _courseProgramService.GetAll().Where(x => x.CourseId == courseId).ToList();

            var toDeletePrograms = oldPrograms.Where(x => !programIds.Contains(x.ProgramId));
            _courseProgramService.DeleteRange(toDeletePrograms);

            var oldIds = oldPrograms.Select(x => x.ProgramId);
            var toInsertPrograms = programIds.Where(x => !oldIds.Contains(x)).Select(x => new CourseProgram() { CourseId = courseId, ProgramId = x });
            _courseProgramService.AddRange(toInsertPrograms);
        }

        
    }
}

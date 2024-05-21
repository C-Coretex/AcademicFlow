using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Managers.Contracts.IManagers;
using AutoMapper;

namespace AcademicFlow.Managers.Managers
{
    public class CourseManager(IMapper mapper, ICourseService courseService, ICourseProgramService courseProgramService, IProgramService programService) 
        : BaseManager(mapper), ICourseManager
    {
        private readonly ICourseService _courseService = courseService;
        private readonly ICourseProgramService _courseProgramService = courseProgramService;
        private readonly IProgramService _programService = programService;

        public int? AddCourse(Course course)
        {
            return _courseService.AddCourse(course);
        }

        public Course? GetCourseById(int id)
        {
            return _courseService.GetCourseById(id);
        }

        public void UpdateCourse(Course course)
        {
            _courseService.UpdateCourse(course);
        }
    }
}

using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Managers.Contracts.Models.CourseModels;
using AutoMapper;

namespace AcademicFlow.Managers.Profiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseTableItem>();
        }
    }
}

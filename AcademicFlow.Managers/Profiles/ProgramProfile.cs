using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Managers.Contracts.Models.ProgramModels;
using AutoMapper;

namespace AcademicFlow.Managers.Profiles
{
    public class ProgramProfile : Profile
    {
        public ProgramProfile()
        {
            CreateMap<Program, ProgramTableItem>()
                .ForMember(dst => dst.CourseNames, opt => opt
                    .MapFrom(src => src.Courses != null ? src.Courses.Where(x => x.ProgramId == src.Id && x.Course != null).Select(x => x.Course.Name) : null));
        }
    }
}

using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Managers.Contracts.Models.ProgramModels;
using AutoMapper;

namespace AcademicFlow.Managers.Profiles
{
    public class ProgramProfile : Profile
    {
        public ProgramProfile()
        {
            CreateMap<Program, ProgramTableItem>();
            CreateMap<Program, ProgramWebModel>()
                //.ForMember(dst => dst.Users, opt => opt.MapFrom(src => src.UserRoles.Select(x => x.UserRole.User).ToList()))
                ;
        }
    }
}

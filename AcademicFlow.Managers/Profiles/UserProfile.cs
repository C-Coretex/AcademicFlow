using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.Models.UserModels;
using AutoMapper;

namespace AcademicFlow.Managers.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserWebModel>()
                .ForMember(x => x.UserRegistrationData, opt => opt.MapFrom(x => new UserRegistrationData(x.UserCredentials)))
                .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.UserRoles.Select(x => x.Role.ToString())));
            CreateMap<User, UserListModel>()
                .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.UserRoles.Select(x => x.Role.ToString())));

        }
    }
}

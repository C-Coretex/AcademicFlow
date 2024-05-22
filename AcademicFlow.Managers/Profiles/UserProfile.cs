using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.Models.UserModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicFlow.Managers.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserWebModel>()
                .ForMember(x => x.UserRegistrationData, opt => opt.MapFrom(x => new UserRegistrationData(x.UserCredentials)))
                .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.UserRoles.Select(x => x.Role.ToString())));
        }
    }
}

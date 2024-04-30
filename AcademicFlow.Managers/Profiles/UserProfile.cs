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
            CreateMap<User, UserWebModel>();
        }
    }
}

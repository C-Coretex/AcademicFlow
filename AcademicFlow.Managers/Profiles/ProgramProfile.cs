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
        }
    }
}

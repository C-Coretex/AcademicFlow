using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Managers.Contracts.Models.AssignmentModels.InputModels;
using AutoMapper;

namespace AcademicFlow.Managers.Profiles
{
    public class AssignmentProfile : Profile
    {
        public AssignmentProfile()
        {
            CreateMap<AssignmentTaskInputModel, AssignmentTask>();
            CreateMap<AssignmentGradeInputModel, AssignmentGrade>();
        }
    }
}

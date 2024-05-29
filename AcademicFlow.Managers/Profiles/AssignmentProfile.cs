using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Managers.Contracts.Models.AssignmentModels.InputModels;
using AcademicFlow.Managers.Contracts.Models.AssignmentModels.OutputModels;
using AutoMapper;

namespace AcademicFlow.Managers.Profiles
{
    public class AssignmentProfile : Profile
    {
        public AssignmentProfile()
        {
            CreateMap<AssignmentTaskInputModel, AssignmentTask>();
            CreateMap<AssignmentGradeInputModel, AssignmentGrade>();

            CreateMap<AssignmentTask, AssignmentTaskOutputModel>()
                .ForMember(x => x.CreatedBy, opt => opt.MapFrom(x => x.User));

            CreateMap<AssignmentEntry,  AssignmentEntryOutputModel>()
                .ForMember(x => x.FileName, opt => opt.MapFrom(x => Path.GetFileName(x.AssignmentFilePath)))
                .ForMember(x => x.CreatedBy, opt => opt.MapFrom(x => x.User));

            CreateMap<AssignmentGrade, AssignmentGradeOutputModel>()
                .ForMember(x => x.GradedBy, opt => opt.MapFrom(x => x.User));
        }
    }
}

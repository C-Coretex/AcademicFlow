using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Contracts.Models.ProgramModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace AcademicFlow.Managers.Managers
{
    public class ProgramManager(IMapper mapper, ICourseProgramService courseProgramService, IProgramService programService)
        : BaseManager(mapper), IProgramManager
    {
        private readonly ICourseProgramService _courseProgramService = courseProgramService;
        private readonly IProgramService _programService = programService;

        public int? AddProgram(Program program)
        {
            return _programService.AddProgram(program);
        }

        public void DeleteProgram(int id)
        {
            _programService.DeleteProgram(id);
        }

        public Program? GetProgramById(int id)
        {
            return _programService.GetProgramById(id);
        }

        public void UpdateProgram(Program program)
        {
            _programService.UpdateProgram(program);
        }

        public IEnumerable<ProgramTableItem> GetProgramTableItemList(int? userId)
        {
            var programs = _programService.GetAll();
            if (userId.HasValue)
            {
                programs = programs.Where(x => x.Users != null && x.Users.Any(x => x.User.User.Id == userId));
            }
            return programs.ProjectTo<ProgramTableItem>(MapperConfig).AsEnumerable();
        }
    }
}

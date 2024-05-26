using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Contracts.IServices;

namespace AcademicFlow.Domain.Services
{
    public class ProgramService(IProgramRepository programRepository, IProgramUserRoleRepository programUserRoleRepository) : IProgramService
    {
        private readonly IProgramRepository _programRepository = programRepository;
        private readonly IProgramUserRoleRepository _programUserRoleRepository = programUserRoleRepository;
        public int? AddProgram(Program entity)
        {
            return _programRepository.Add(entity)?.Id;
        }

        public Program? GetProgramById(int id)
        {
            return _programRepository.GetById(id);
        }

        public void UpdateProgram(Program entity)
        {
            _programRepository.Update(entity);
        }

        public IQueryable<Program> GetAll()
        {
            return _programRepository.GetAll();
        }

        public void DeleteProgram(int id)
        {
            _programRepository.Delete(id);
        }

        public void DeleteProgramUserRolesRange(IEnumerable<ProgramUserRole> userRoles)
        {
            _programUserRoleRepository.DeleteRange(userRoles);
        }

        public void AddProgramUserRolesRange(IEnumerable<ProgramUserRole> userRoles)
        {
            _programUserRoleRepository.AddRange(userRoles);
        }

        public IEnumerable<ProgramUserRole> GetAllUserRoles()
        {
            return _programUserRoleRepository.GetAll();
        }
    }
}

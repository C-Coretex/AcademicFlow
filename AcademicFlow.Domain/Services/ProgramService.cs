using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Contracts.IServices;
using Microsoft.EntityFrameworkCore;

namespace AcademicFlow.Domain.Services
{
    public class ProgramService(IProgramRepository programRepository, IProgramUserRoleRepository programUserRoleRepository) : IProgramService
    {
        private readonly IProgramRepository _programRepository = programRepository;
        private readonly IProgramUserRoleRepository _programUserRoleRepository = programUserRoleRepository;
        public async Task<int?> AddProgramAsync(Program entity)
        {
            return (await _programRepository.AddAsync(entity))?.Id;
        }

        public async Task<Program?> GetProgramByIdAsync(int id)
        {
            return await _programRepository.GetByIdAsync(id);
        }

        public async Task UpdateProgramAsync(Program entity)
        {
            await _programRepository.UpdateAsync(entity);
        }

        public IQueryable<Program> GetAll()
        {
            return _programRepository.GetAll().Include(x => x.UserRoles).Include(x => x.Courses).AsNoTracking();
        }

        public async Task DeleteProgramAsync(int id)
        {
            await _programRepository.DeleteAsync(id);
        }

        public async Task DeleteProgramUserRolesRangeAsync(IEnumerable<ProgramUserRole> userRoles)
        {
            await _programUserRoleRepository.DeleteRangeAsync(userRoles);
        }

        public async Task AddProgramUserRolesRangeAsync(IEnumerable<ProgramUserRole> userRoles)
        {
            await _programUserRoleRepository.AddRangeAsync(userRoles);
        }

        public IEnumerable<ProgramUserRole> GetAllUserRoles()
        {
            return _programUserRoleRepository.GetAll().Include(x => x.Program).Include(x => x.UserRole).ThenInclude(x => x.User).AsNoTracking();
        }
    }
}

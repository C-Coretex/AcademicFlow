using AcademicFlow.Domain.Contracts.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface IProgramService
    {
        Task<int?> AddProgramAsync(Program entity);
        Task<Program?> GetProgramByIdAsync(int id);
        Task UpdateProgramAsync(Program entity);
        IQueryable<Program> GetAll();
        IQueryable<Program> GetAllWithUsers();
        Task DeleteProgramAsync(int id);
        Task DeleteProgramUserRolesRangeAsync(IEnumerable<ProgramUserRole> userRoles);
        Task AddProgramUserRolesRangeAsync(IEnumerable<ProgramUserRole> userRoles);
        IEnumerable<ProgramUserRole> GetAllUserRoles();

    }
}

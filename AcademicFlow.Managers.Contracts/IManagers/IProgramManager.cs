using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.Models.ProgramModels;

namespace AcademicFlow.Managers.Contracts.IManagers
{
    public interface IProgramManager
    {
        Task<int?> AddProgramAsync(Program program);
        Task DeleteProgramAsync(int id);
        Task<Program?> GetProgramByIdAsync(int id);
        Task UpdateProgramAsync(Program program);
        IEnumerable<ProgramTableItem> GetProgramTableItemList(int? userId);
        Task EditProgramUserRolesAsync(int programId, int[] usersIds);
        IEnumerable<User> GetProgramUsers(int programId);
    }
}

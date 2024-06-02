using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.Models;
using AcademicFlow.Managers.Contracts.Models.ProgramModels;

namespace AcademicFlow.Managers.Contracts.IManagers
{
    public interface IProgramManager
    {
        Task<int?> AddProgramAsync(Program program);

        Task DeleteProgramAsync(int id);
        
        Program? GetProgramById(int id);
        ProgramWebModel GetProgramWebModel(int id);

        Task UpdateProgramAsync(Program program);

        IEnumerable<ProgramTableItem> GetProgramTableItemList(int? userId);

        Task<ResponseModel> EditProgramUserRolesAsync(int programId, int[] usersIds);

        IEnumerable<User> GetProgramUsers(int programId);
    }
}

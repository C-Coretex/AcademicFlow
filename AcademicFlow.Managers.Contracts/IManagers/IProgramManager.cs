using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.Models.ProgramModels;

namespace AcademicFlow.Managers.Contracts.IManagers
{
    public interface IProgramManager
    {
        int? AddProgram(Program program);
        void DeleteProgram(int id);
        Program? GetProgramById(int id);
        void UpdateProgram(Program program);
        IEnumerable<ProgramTableItem> GetProgramTableItemList(int? userId, RolesEnum? role);
        void EditProgramUserRoles(int programId, int[] usersIds);
        IEnumerable<User> GetProgramUsers(int programId);
    }
}

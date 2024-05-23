using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Managers.Contracts.Models.ProgramModels;

namespace AcademicFlow.Managers.Contracts.IManagers
{
    public interface IProgramManager
    {
        int? AddProgram(Program program);
        void DeleteProgram(int id);
        Program? GetProgramById(int id);
        void UpdateProgram(Program program);
        IEnumerable<ProgramTableItem> GetProgramTableItemList(int? userId);
    }
}

using AcademicFlow.Domain.Contracts.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface IProgramService
    {
        int? AddProgram(Program entity);
        Program? GetProgramById(int id);
        void UpdateProgram(Program entity);
        IQueryable<Program> GetAll();
        void DeleteProgram(int id);
        void DeleteProgramUserRolesRange(IEnumerable<ProgramUserRole> userRoles);
        void AddProgramUserRolesRange(IEnumerable<ProgramUserRole> userRoles);

    }
}

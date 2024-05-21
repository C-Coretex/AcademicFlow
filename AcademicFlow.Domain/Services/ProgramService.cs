using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Contracts.IServices;

namespace AcademicFlow.Domain.Services
{
    public class ProgramService(IProgramRepository programRepository, IProgramUserRoleRepository programUserRoleRepository) : IProgramService
    {
        private readonly IProgramRepository _programRepository = programRepository;
        private readonly IProgramUserRoleRepository _programUserRoleRepository = programUserRoleRepository;
    }
}

using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Helpers.Base;

namespace AcademicFlow.Infrastructure.Repositories
{
    public class ProgramUserRoleRepository(AcademicFlowDbContext dbContext)
        : RepositoryBase<AcademicFlowDbContext, ProgramUserRole>(dbContext, dbContext.ProgramUserRole), IProgramUserRoleRepository
    {
    }
}

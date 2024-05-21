using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Helpers.Base;

namespace AcademicFlow.Infrastructure.Repositories
{
    public class ProgramRepository(AcademicFlowDbContext dbContext)
        : RepositoryBase<AcademicFlowDbContext, Program>(dbContext, dbContext.Program), IProgramRepository
    {
    }
}

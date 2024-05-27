using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Helpers.Base;

namespace AcademicFlow.Infrastructure.Repositories
{
    public class UserCredentialsRepository(AcademicFlowDbContext dbContext) 
        : RepositoryBase<AcademicFlowDbContext, UserCredentials>(dbContext, dbContext.UserCredentials), IUserCredentialsRepository
    {
    }
}

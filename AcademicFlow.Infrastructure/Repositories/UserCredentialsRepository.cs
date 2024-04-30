using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Helpers.Base;

namespace AcademicFlow.Infrastructure.Repositories
{
    public class UserCredentialsRepository : RepositoryBase<AcademicFlowDbContext, UserCredentials>, IUserCredentialsRepository
    {
        public UserCredentialsRepository(AcademicFlowDbContext dbContext) : base(dbContext, dbContext.UserCredentials)
        {
        }
    }
}

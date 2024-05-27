using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Domain.Helpers.Base;

namespace AcademicFlow.Infrastructure.Repositories
{
    public class UserRepository(AcademicFlowDbContext dbContext) : RepositoryBase<AcademicFlowDbContext, User>(dbContext, dbContext.User), IUserRepository
    {
    }
}

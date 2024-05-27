using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Helpers.Base;

namespace AcademicFlow.Infrastructure.Repositories
{
    public class UserRoleRepository(AcademicFlowDbContext dbContext) : RepositoryBase<AcademicFlowDbContext, UserRole>(dbContext, dbContext.UserRole), IUserRoleRepository 
    {
    }
}

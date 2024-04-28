using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Domain.Helpers.Base;
using Microsoft.EntityFrameworkCore;

namespace AcademicFlow.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<AcademicFlowDbContext, User>, IUserRepository
    {
        public UserRepository(AcademicFlowDbContext dbContext, DbSet<User> dbSet) : base(dbContext, dbSet)
        {
        }
    }
}

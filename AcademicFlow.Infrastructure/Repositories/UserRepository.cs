﻿using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Domain.Helpers.Base;

namespace AcademicFlow.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<AcademicFlowDbContext, User>, IUserRepository
    {
        public UserRepository(AcademicFlowDbContext dbContext) : base(dbContext, dbContext.User)
        {
        }
    }
}
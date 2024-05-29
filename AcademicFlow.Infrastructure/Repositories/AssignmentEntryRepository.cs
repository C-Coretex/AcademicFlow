﻿using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Helpers.Base;

namespace AcademicFlow.Infrastructure.Repositories
{
    public class AssignmentEntryRepository(AcademicFlowDbContext dbContext) : RepositoryBase<AcademicFlowDbContext, AssignmentEntry>(dbContext, dbContext.AssignmentEntry), IAssignmentEntryRepository
    {
    }
}
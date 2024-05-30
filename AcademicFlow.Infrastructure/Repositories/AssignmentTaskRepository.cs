using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Helpers.Base;

namespace AcademicFlow.Infrastructure.Repositories
{
    public class AssignmentTaskRepository(AcademicFlowDbContext dbContext) : RepositoryBase<AcademicFlowDbContext, AssignmentTask>(dbContext, dbContext.AssignmentTask), IAssignmentTaskRepository
    {
    }
}

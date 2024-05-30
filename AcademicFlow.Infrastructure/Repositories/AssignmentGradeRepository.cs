using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Helpers.Base;

namespace AcademicFlow.Infrastructure.Repositories
{
    public class AssignmentGradeRepository(AcademicFlowDbContext dbContext) : RepositoryBase<AcademicFlowDbContext, AssignmentGrade>(dbContext, dbContext.AssignmentGrade), IAssignmentGradeRepository
    {
    }
}

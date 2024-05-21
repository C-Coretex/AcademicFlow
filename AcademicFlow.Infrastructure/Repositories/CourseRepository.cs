using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Helpers.Base;

namespace AcademicFlow.Infrastructure.Repositories
{
    public class CourseRepository(AcademicFlowDbContext dbContext) 
        : RepositoryBase<AcademicFlowDbContext, Course>(dbContext, dbContext.Course), ICourseRepository
    {
    }
}

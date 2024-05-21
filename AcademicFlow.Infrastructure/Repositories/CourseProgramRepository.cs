using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Helpers.Base;

namespace AcademicFlow.Infrastructure.Repositories
{
    public class CourseProgramRepository(AcademicFlowDbContext dbContext) 
        : RepositoryBase<AcademicFlowDbContext, CourseProgram>(dbContext, dbContext.CourseProgram), ICourseProgramRepository
    {
    }
}

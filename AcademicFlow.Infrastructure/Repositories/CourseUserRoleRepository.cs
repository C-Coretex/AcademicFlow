using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Helpers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicFlow.Infrastructure.Repositories
{
    public class CourseUserRoleRepository(AcademicFlowDbContext dbContext) 
        : RepositoryBase<AcademicFlowDbContext, CourseUserRole>(dbContext, dbContext.CourseUserRole), ICourseUserRoleRepository
    {
    }
}

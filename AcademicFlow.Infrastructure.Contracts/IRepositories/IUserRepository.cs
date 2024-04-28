using AcademicFlow.Domain.Helpers.Base;
using AcademicFlow.Domain.Entities;

namespace AcademicFlow.Infrastructure.Contracts.IRepositories
{
    public interface IUserRepository: RepositoryBase<AcademicFlowDbContext, User>
    {
    }
}

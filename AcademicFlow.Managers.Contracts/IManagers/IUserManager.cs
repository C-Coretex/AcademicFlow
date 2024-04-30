using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.Models.UserModels;

namespace AcademicFlow.Managers.Contracts.IManagers
{
    public interface IUserManager
    {
        Task AddUser(User user);
        IAsyncEnumerable<UserWebModel> GetUsers();
    }
}

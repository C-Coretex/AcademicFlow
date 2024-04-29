using AcademicFlow.Domain.Entities;

namespace AcademicFlow.Managers.Contracts.IManagers
{
    public interface IUserManager
    {
        Task AddUser(User user);
        IAsyncEnumerable<User> GetUsers();
    }
}

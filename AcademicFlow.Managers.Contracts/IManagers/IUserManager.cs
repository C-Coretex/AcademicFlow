using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.Models.UserModels;

namespace AcademicFlow.Managers.Contracts.IManagers
{
    public interface IUserManager
    {
        IAsyncEnumerable<UserWebModel> GetUsers();

        Task<User> GetUserByPersonalCode(string PersonalCode);
        Task DeleteUser(string personalCode);
        /// <returns>Security key of the user</returns>
        Task<string> AddUser(User user);
        IAsyncEnumerable<UserWebModel> GetUsers(string controllerUrl);
    }
}

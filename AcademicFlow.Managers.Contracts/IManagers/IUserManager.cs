using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.Models.UserModels;

namespace AcademicFlow.Managers.Contracts.IManagers
{
    public interface IUserManager
    {
        Task<User> GetUserById(int userId);

        Task DeleteUser(int userId);

        /// <returns>Security key of the user</returns>
        Task<string> AddUser(User user);

        IAsyncEnumerable<UserWebModel> GetUsers(string controllerUrl);

        Task UpdateRoles(int userId, IEnumerable<RolesEnum> roles);

        Task UpdateUser(User user);
    }
}


using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.Models.UserModels;

namespace AcademicFlow.Managers.Contracts.IManagers
{
    public interface IUserManager
    {
        /// <returns>Security key of the user</returns>
        Task<string> AddUser(User user);
        IAsyncEnumerable<UserWebModel> GetUsers(string controllerUrl);
    }
}

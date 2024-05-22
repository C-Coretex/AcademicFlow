using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.Models.UserModels;

namespace AcademicFlow.Managers.Contracts.IManagers
{
    public interface IUserCredentialsManager
    {
        Task<User> RegisterUser(string secretKey, string username, string password);
        Task<UserWebModel> LoginUser(string username, string password);
        Task<UserWebModel> GetUserBySecretKey(string secretKey);
        Task<UserCredentials> AddUserCredential(int userId);
        Task DeleteUserCredential(int userId);
    }
}

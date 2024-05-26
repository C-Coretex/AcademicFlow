using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface IUserCredentialsService
    {
        Task AddUserCredentials(int userId, string username, string password);
        Task SaveUserCredentials(UserCredentials userCredentials);
        Task<bool> IsUserCredentialsValid(string username, string password);
        Task<bool> IsUsernameTaken(string username);
        Task<User?> GetUserByCredentials(string username, string password);
        Task<User?> GetUserBySecretKey(string secretKey);
        Task<UserCredentials?> GetUserCredentialsById(int userId);
        Task<UserCredentials?> GetUserCredentialsBySecretKey(string secretKey);
    }
}

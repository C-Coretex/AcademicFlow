using AcademicFlow.Domain.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface IUserCredentialsService
    {
        Task AddUserCredentials(int userId, string username, string password);
        Task<bool> IsUserCredentialsValid(string username, string password);
        Task<bool> IsUsernameTaken(string username);
        Task<User?> GetUserByCredentials(string username, string password);
    }
}

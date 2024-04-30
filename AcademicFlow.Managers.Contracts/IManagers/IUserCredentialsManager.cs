using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.Models.UserModels;

namespace AcademicFlow.Managers.Contracts.IManagers
{
    public interface IUserCredentialsManager
    {
        Task RegisterUser(int userId, string username, string password);
        Task<UserWebModel> LoginUser(string username, string password);
    }
}

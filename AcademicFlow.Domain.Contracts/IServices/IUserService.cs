using AcademicFlow.Domain.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface IUserService
    {
        IQueryable<User> GetUsers();
        Task AddUser(User user);
    }
}

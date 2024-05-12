using AcademicFlow.Domain.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface IUserService
    {
        Task<User?> GetById(int id);
        IQueryable<User> GetUsers();
        Task<User> AddUser(User user);
        Task UpdateUser(User user);
    }
}

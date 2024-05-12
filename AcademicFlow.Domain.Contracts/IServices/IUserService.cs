using AcademicFlow.Domain.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface IUserService
    {
        Task<User?> GetById(int id);
        IQueryable<User> GetUsers();
        Task<User> GetUserByPersonalCode(string personalCode);
        void DeleteUser(string personalCode);
        Task<User> AddUser(User user);
        Task UpdateUser(User user);
    }
}

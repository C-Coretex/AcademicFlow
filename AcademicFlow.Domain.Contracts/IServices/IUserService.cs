using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface IUserService
    {
        Task<User?> GetById(int id);
        IQueryable<User> GetUsers();
        IQueryable<User> GetUsersWithRoles();

        Task<User> GetUserById(int userId);
        Task<User?> GetUserByIdWithAssignments(int userId);
        Task DeleteUser(int userId);
        Task<User> AddUser(User user);
        Task UpdateRoles(int userId, IEnumerable<RolesEnum> roles);
        Task UpdateUser(User user);
    }
}

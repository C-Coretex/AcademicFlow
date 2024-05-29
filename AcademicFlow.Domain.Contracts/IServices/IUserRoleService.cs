using AcademicFlow.Domain.Contracts.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface IUserRoleService
    {
        Task<UserRole?> GetByUserId(int userId, int courseId);
    }
}

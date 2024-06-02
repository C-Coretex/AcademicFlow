using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Contracts.IServices;
using Microsoft.EntityFrameworkCore;

namespace AcademicFlow.Domain.Services
{
    public class UserRoleService: IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public Task<UserRole?> GetByUserId(int userId, int courseId)
        {
            return _userRoleRepository.GetAll().Include(x => x.Courses).Include(x => x.Programs).ThenInclude(x => x.Program)
                            .ThenInclude(x => x.Courses).FirstOrDefaultAsync(x => x.UserId == userId && (x.Courses.Any(y => y.CourseId == courseId) || x.Programs.Any(y => y.Program.Courses.Any(z => z.CourseId == courseId))));
        }
    }
}
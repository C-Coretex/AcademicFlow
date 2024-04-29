using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.IManagers;
using Microsoft.EntityFrameworkCore;

namespace AcademicFlow.Managers.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserService _userService;
        public UserManager(IUserService userService)
        {
            _userService = userService;
        }

        public async Task AddUser(User user)
        {
            await _userService.AddUser(user);
        }

        public IAsyncEnumerable<User> GetUsers()
        {
            return _userService.GetUsers().AsAsyncEnumerable();
        }
    }
}

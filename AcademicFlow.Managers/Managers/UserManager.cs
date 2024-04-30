using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Contracts.Models.UserModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AcademicFlow.Managers.Managers
{
    public class UserManager : BaseManager, IUserManager
    {
        private readonly IUserService _userService;
        public UserManager(IMapper mapper, IUserService userService): base(mapper)
        {
            _userService = userService;
        }

        public async Task AddUser(User user)
        {
            await _userService.AddUser(user);
        }

        public IAsyncEnumerable<UserWebModel> GetUsers()
        {
            return _userService.GetUsers().ProjectTo<UserWebModel>(MapperConfig).AsAsyncEnumerable();
        }
    }
}

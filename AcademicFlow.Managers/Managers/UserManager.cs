using AcademicFlow.Domain.Contracts.Constants;
using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Domain.Helpers.Helpers;
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
        private readonly IUserCredentialsManager _userCredentialsManager;
        public UserManager(IMapper mapper, IUserService userService, IUserCredentialsManager userCredentialsManager): base(mapper)
        {
            _userService = userService;
            _userCredentialsManager = userCredentialsManager;
        }

        public async Task<string> AddUser(User user)
        {
            user = await _userService.AddUser(user);

            

            return securityKey;
        }

        public async IAsyncEnumerable<UserWebModel> GetUsers(string controllerUrl)
        {
            var users = _userService.GetUsers().ProjectTo<UserWebModel>(MapperConfig).AsAsyncEnumerable();
            await foreach(var user in users)
            {
                if(!user.UserRegistrationData.IsRegistered && !string.IsNullOrEmpty(user.UserRegistrationData.RegistrationUrl))
                    user.UserRegistrationData.RegistrationUrl = controllerUrl + user.UserRegistrationData.RegistrationUrl;

                yield return user;
            }
        }
    }
}

using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Contracts.Models.UserModels;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;

namespace AcademicFlow.Managers.Managers
{
    public class UserCredentialsManager: BaseManager, IUserCredentialsManager
    {
        private readonly IUserCredentialsService _userCredentialsService;
        private readonly IUserService _userService;
        public UserCredentialsManager(IMapper mapper, IUserCredentialsService userCredentialsService, IUserService userService) : base(mapper)
        {
            _userCredentialsService = userCredentialsService;
            _userService = userService;
        }

        public async Task<User> RegisterUser(string secretKey, string username, string password)
        {
            if(string.IsNullOrEmpty(secretKey))
                throw new ValidationException("SecretKey must is required");

            var user = await _userCredentialsService.GetUserBySecretKey(secretKey);
            if (user == null)
                throw new ValidationException("User has not been created or secretKey is invalid");

            var isUsernameTaken = await _userCredentialsService.IsUsernameTaken(username);
            if(isUsernameTaken)
                throw new ValidationException("Username is already taken");

            await _userCredentialsService.AddUserCredentials(user.Id, username, password);

            return user;
        }

        public async Task<UserWebModel> LoginUser(string username, string password)
        {
            var user = await _userCredentialsService.GetUserByCredentials(username, password);
            if (user == null)
                throw new AuthenticationException("Username or password incorrect");

            return Mapper.Map<UserWebModel>(user);
        }

        public async Task<UserWebModel> GetUserBySecretKey(string secretKey)
        {
            var user = await _userCredentialsService.GetUserBySecretKey(secretKey);
            if (user == null)
                throw new AuthenticationException("Username or password incorrect");

            return Mapper.Map<UserWebModel>(user);
        }
    }
}

using AcademicFlow.Domain.Contracts.Constants;
using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Domain.Helpers.Helpers;
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

        public async Task<UserCredentials> GetUserCredentialsBySecretKey(string secretKey)
        {
            var userCredentials = await _userCredentialsService.GetUserCredentialsBySecretKey(secretKey);
            if (userCredentials == null)
                throw new AuthenticationException("Username or password incorrect");

            return userCredentials;
        }

        public async Task<UserWebModel> GetUserWebModelBySecretKey(string secretKey)
        {
            var user = await _userCredentialsService.GetUserBySecretKey(secretKey);
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

        public async Task<UserCredentials> GetUserCredentialsById(int userId)
        {
            var userCredentials = await _userCredentialsService.GetUserCredentialsById(userId);
            if (userCredentials == null)
                throw new ValidationException("User credentials not found");
                
            return userCredentials;
        }

        public async Task<string?> ResetUserCredentials(int userId)
        {
            var securityKey = CryptographyHelper.GetRandomString(UserConstants.SecurityKeySize);
            var userCredentials = await GetUserCredentialsById(userId);
            if (userCredentials == null)
                throw new ValidationException("User credentials not found");

            userCredentials.SecurityKey = securityKey;
            await _userCredentialsService.SaveUserCredentials(userCredentials);
            if (userCredentials != null)
                return securityKey;
            return null;
         }

        public async Task<UserCredentials> UpdateUserCredentials(string securityKey, string password)
        {
            var userCredentials = await GetUserCredentialsBySecretKey(securityKey);
            if (userCredentials == null)
                throw new ValidationException("User credentials not found");

            var (hash, salt) = CryptographyHelper.GetHash(password);
            userCredentials.PasswordHash = hash;
            userCredentials.Salt = salt;
            await _userCredentialsService.SaveUserCredentials(userCredentials);

            return userCredentials;
        }
    }
}

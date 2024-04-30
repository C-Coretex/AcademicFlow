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
        public UserCredentialsManager(IMapper mapper, IUserCredentialsService userCredentialsService): base(mapper)
        {
            _userCredentialsService = userCredentialsService;
        }

        public async Task RegisterUser(int userId, string username, string password)
        {
            var isUsernameTaken = await _userCredentialsService.IsUsernameTaken(username);
            if(isUsernameTaken)
                throw new ValidationException("Username is already taken");

            await _userCredentialsService.AddUserCredentials(userId, username, password);
        }

        public async Task<UserWebModel> LoginUser(string username, string password)
        {
            var user = await _userCredentialsService.GetUserByCredentials(username, password);
            if (user == null)
                throw new AuthenticationException("Username or password incorrect");

            return Mapper.Map<UserWebModel>(user);
        }
    }
}

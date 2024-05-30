using AcademicFlow.Helpers;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Contracts.Models.UserModels;
using AcademicFlow.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;

namespace AcademicFlow.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthorizationController : Controller
    {
        private readonly IUserCredentialsManager _userCredentialsManager;
        private readonly IUserManager _userManager;
        private readonly ILogger<AuthorizationController> _logger;
        public AuthorizationController(IUserCredentialsManager userCredentialsManager, IUserManager userManager, ILogger<AuthorizationController> logger)
        {
            _userCredentialsManager = userCredentialsManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] UserModel userModel)
        {
            try
            {
                var user = await _userCredentialsManager.RegisterUser(userModel.SecretKey, userModel.Username, userModel.Password);
                AuthorizationHelpers.LoginUser(HttpContext.Session, user.Id);

                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while registering user");
                return BadRequest(e.Message);
            }
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] UserModel userModel)
        {
            try
            {
                var user = await _userCredentialsManager.LoginUser(userModel.Username, userModel.Password);
                AuthorizationHelpers.LoginUser(HttpContext.Session, user.Id);

                return Ok(user);
            }
            catch (AuthenticationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while logging in");
                return BadRequest(e.Message);
            }
        }

        [HttpGet("LogoutUser")]
        public IActionResult LogoutUser()
        {
            try
            {
                AuthorizationHelpers.LogoutUser(HttpContext.Session);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while logging out");
                return BadRequest(e.Message);
            }
        }

        [HttpGet("PasswordReset")]
        public async Task<IActionResult> PasswordReset([FromBody] UserModel userModel)
        {
            try
            {
                var userCredentials = await _userCredentialsManager.UpdateUserCredentials(userModel.SecretKey, userModel.Password);
                AuthorizationHelpers.LoginUser(HttpContext.Session, userCredentials.Id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while entering new password");
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var userId = AuthorizationHelpers.GetUserIdIfAuthorized(HttpContext.Session);
                UserWebModel? user = null;
                if (userId.HasValue)
                    user = await _userManager.GetUserModelById(userId.Value);

                return Ok(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while fetching user data");
                return BadRequest(e.Message);
            }
        }
    }
}

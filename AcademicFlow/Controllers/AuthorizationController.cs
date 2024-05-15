using AcademicFlow.Domain.Entities;
using AcademicFlow.Helpers;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Models;
using AcademicFlow.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;

namespace AcademicFlow.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthorizationController : Controller
    {
        private readonly IUserCredentialsManager _userCredentialsManager;
        private readonly ILogger<AuthorizationController> _logger;
        public AuthorizationController(IUserCredentialsManager userCredentialsManager, ILogger<AuthorizationController> logger)
        {
            _userCredentialsManager = userCredentialsManager;
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
            catch(AuthenticationException e)
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
    }
}

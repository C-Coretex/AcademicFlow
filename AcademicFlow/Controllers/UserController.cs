using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Filters;
using AcademicFlow.Helpers;
using AcademicFlow.Managers.Contracts.IManagers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademicFlow.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ILogger<UserController> _logger;
        private readonly IUserCredentialsManager _userCredentialsManager;
        public UserController(IUserManager userManager, ILogger<UserController> logger, IUserCredentialsManager userCredentialsManager)
        {
            _userManager = userManager;

            _logger = logger;
            _userCredentialsManager = userCredentialsManager;
        }

        [AuthorizeUser(RolesEnum.Admin)]
        [HttpPut("AddUser")]
        public async Task<IActionResult> AddUser([FromForm] string name, [FromForm] string surname, [FromForm]  string personalCode, [FromForm] string? email, [FromForm] string? phoneNumber, [FromForm]  int? age)
        {
            try
            {
                var user = new User(name, surname, personalCode, email, phoneNumber, age);
                var securityKey = await _userManager.AddUser(user);

                var userRegistrationEndpoint = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/Home/UserRegistration?secretKey={securityKey}";

                return Ok(userRegistrationEndpoint);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting users");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser]
        [HttpPost("EditUser")]
        public async Task<IActionResult> EditUser([FromForm] int? id, [FromForm] string name, [FromForm] string surname, [FromForm] string personalCode, [FromForm] string? email, [FromForm] string? phoneNumber, [FromForm] int? age)
        {
            try
            {
                var currentUserID = AuthorizationHelpers.GetUserIdIfAuthorized(HttpContext.Session);
                id ??= currentUserID;
                var user = await _userManager.GetUserById(id!.Value);
                if (user == null)
                {
                    var message = $"User {id} do not exist";
                    _logger.LogError(message);
                    return BadRequest(message);
                }

                if (id != currentUserID && !AuthorizationHelpers.HasRole([RolesEnum.Admin], user))
                    throw new UnauthorizedAccessException("Non admin cannot change different user");

                user.Name = name;
                user.Surname = surname;
                user.PhoneNumber = phoneNumber;
                user.Age = age;
                user.PersonalCode = personalCode;
                user.Email = email;
                await _userManager.UpdateUser(user);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting users");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser(RolesEnum.Admin,RolesEnum.Student)]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var userRegistrationEndpoint = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/Home/UserRegistration?secretKey=";
                var data = await _userManager.GetUsers(userRegistrationEndpoint).ToListAsync();

                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting users");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser]
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserByUserId([FromForm] int userId)
        {
            try
            {
                var user = await _userManager.GetUserById(userId);

                return Ok(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting user by user id");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser]
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromForm] int userId)
        {
            try
            {
                var user = _userManager.GetUserById(userId);
                await _userManager.DeleteUser(userId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while deleting users");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser]
        [HttpPost("ChangeRoles")]
        public async Task<IActionResult> ChangeRole([FromForm] int userId, [FromForm] RolesEnum[] roles)
        {
            try
            {
                var user = await _userManager.GetUserById(userId);

                await _userManager.UpdateRoles(userId, roles);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while changing role");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser(RolesEnum.Admin)]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] int userId)
        {
            try
            {
                var user = await _userManager.GetUserById(userId);
                if (user == null)
                {
                    var message = $"User do not exist";
                    _logger.LogError(message);
                    return BadRequest(message);
                }
                var securityKey = await _userCredentialsManager.ResetUserCredentials(userId);
                var resetPasswordEndpoint = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/Home/UserPasswordReset?secretKey={securityKey}";
                
                return Ok(resetPasswordEndpoint);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while resetting password");
                return BadRequest(e.Message);
            }
        }
    }
}

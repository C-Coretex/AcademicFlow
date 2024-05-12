using AcademicFlow.Domain.Entities;
using AcademicFlow.Filters;
using AcademicFlow.Managers.Contracts.IManagers;
using Microsoft.AspNetCore.Mvc;

namespace AcademicFlow.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserManager userManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

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
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var data = await _userManager.GetUsers().ToListAsync();

                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting users");
                return BadRequest(e.Message);
            }
        }
    }
}

using AcademicFlow.Managers.Contracts.IManagers;
using Microsoft.AspNetCore.Mvc;

namespace AcademicFlow.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentManager _assignmentManager;
        private readonly ILogger<AssignmentController> _logger;

        public AssignmentController(IAssignmentManager assignmentManager, ILogger<AssignmentController> logger)
        {
            _assignmentManager = assignmentManager;
            _logger = logger;
        }

        /*
         
        [AuthorizeUser(RolesEnum.Admin)]
        [HttpPut("AddUser")]
        public async Task<IActionResult> AddUser([FromForm] string name, [FromForm] string surname, [FromForm] string personalCode, [FromForm] string? email, [FromForm] string? phoneNumber, [FromForm] int? age)
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
         
         */
    }
}

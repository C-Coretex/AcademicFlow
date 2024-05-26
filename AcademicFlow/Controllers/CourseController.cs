using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Filters;
using AcademicFlow.Managers.Contracts.IManagers;
using Microsoft.AspNetCore.Mvc;

namespace AcademicFlow.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CourseController(ILogger<CourseController> logger, ICourseManager courseManager) : Controller
    {
        private readonly ILogger<CourseController> _logger = logger;
        private readonly ICourseManager _courseManager = courseManager;

        [AuthorizeUser(RolesEnum.Admin)]
        [HttpPut("AddCourse")]
        public async Task<IActionResult> AddCourse([FromForm] string name, [FromForm] string description, [FromForm] int creditPoints, [FromForm] string publicId, [FromForm] string? imageUrl)
        {
            try
            {
                var course = new Course(name, description, creditPoints, publicId, imageUrl);
                var courseId = await _courseManager.AddCourseAsync(course);

                return Ok(courseId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Course cretion error");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser(RolesEnum.Admin, RolesEnum.Professor)]
        [HttpPost("EditCourse")]
        public async Task<IActionResult> EditCourse([FromForm] int id, [FromForm] string name, [FromForm] string description, [FromForm] int creditPoints, [FromForm] string publicId,
            [FromForm] string? imageUrl)
        {
            try
            {
                var course = await _courseManager.GetCourseByIdAsync(id);
                if (course == null)
                {
                    var message = $"Course {id} do not exist";
                    _logger.LogError(message);
                    return BadRequest(message);
                }
                course.Name = name;
                course.Description = description;
                course.CreditPoints = creditPoints;
                course.PublicId = publicId;
                course.ImageUrl = imageUrl;
                await _courseManager.UpdateCourseAsync(course);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Course editing error");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser(RolesEnum.Admin, RolesEnum.Professor, RolesEnum.Student)]
        [HttpGet("GetCourse")]
        public async Task<IActionResult> GetCourse(int id)
        {
            try
            {
                var course = await _courseManager.GetCourseByIdAsync(id);
                return PartialView("Partials/_CourseItem", course);  /// return html content
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Course getting error");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser(RolesEnum.Admin, RolesEnum.Professor, RolesEnum.Student)]
        [HttpGet("GetCourseTable")]
        public IActionResult GetCourseTable(int? assignedUserId = null, int? assingedProgramId = null, RolesEnum? role = null)
        {
            try
            {
                var courses = _courseManager.GetCourseTableItemList(assignedUserId, role, assingedProgramId);
                return PartialView("Partials/_CourseTable", courses); /// return html content
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Course table getting error");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser(RolesEnum.Admin)]
        [HttpDelete("DeleteCourse")]
        public async Task<IActionResult> DeleteCourse([FromForm] int id)
        {
            try
            {
                await _courseManager.DeleteCourseAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Course delete error");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser(RolesEnum.Admin)]
        [HttpPost("EditCoursePrograms")]
        public async Task<IActionResult> EditCoursePrograms([FromForm] int id, [FromForm] int[] progamIds)
        {
            try
            {
                await _courseManager.EditCourseProgramsAsync(id, progamIds);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Course editing error");
                return BadRequest(e.Message);
            }
        }

        /// <param name="role">Assing user as professor or as student</param>
        [AuthorizeUser(RolesEnum.Admin)]
        [HttpPost("EditCourseUserRoles")]
        public async Task<IActionResult> EditCourseUserRoles([FromForm] int id, [FromForm] int[] userIds, [FromForm] RolesEnum role)
        {
            try
            {
                await _courseManager.EditCourseUserRoles(id, userIds, role);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Course editing error");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser]
        [HttpGet("GetCourseUsers")]
        public IActionResult GetCourseUsers(int courseId, RolesEnum role)
        {
            try
            {
                var users =  _courseManager.GetCourseUsers(courseId, role);
                return Ok(users);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get Course Users Error");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser]
        [HttpGet("GetCoursePrograms")]
        public IActionResult GetCoursePrograms(int courseId)
        {
            try
            {
                var programs = _courseManager.GetCoursePrograms(courseId);
                return Ok(programs);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get Program Users Error");
                return BadRequest(e.Message);
            }
        }
    }
}

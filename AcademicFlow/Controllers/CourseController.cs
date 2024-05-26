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
        public IActionResult AddCourse([FromForm] string name, [FromForm] string description, [FromForm] int creditPoints, [FromForm] string publicId, [FromForm] string? imageUrl)
        {
            try
            {
                var course = new Course(name, description, creditPoints, publicId, imageUrl);
                var courseId = _courseManager.AddCourse(course);

                return Ok(courseId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Course cretion error");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser(RolesEnum.Admin, RolesEnum.Proffesor)]
        [HttpPost("EditCourse")]
        public IActionResult EditCourse([FromForm] int id, [FromForm] string name, [FromForm] string description, [FromForm] int creditPoints, [FromForm] string publicId,
            [FromForm] string? imageUrl)
        {
            try
            {
                var course = _courseManager.GetCourseById(id);
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
                _courseManager.UpdateCourse(course);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Course editing error");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser(RolesEnum.Admin, RolesEnum.Proffesor, RolesEnum.Student)]
        [HttpGet("GetCourse")]
        public IActionResult GetCourse(int id)
        {
            try
            {
                var course = _courseManager.GetCourseById(id);
                return PartialView("Partials/_CourseItem", course);  /// return html content
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Course getting error");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser(RolesEnum.Admin, RolesEnum.Proffesor, RolesEnum.Student)]
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
        public IActionResult DeleteCourse([FromForm] int id)
        {
            try
            {
                _courseManager.DeleteCourse(id);
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
        public IActionResult EditCoursePrograms([FromForm] int id, [FromForm] int[] progamIds)
        {
            try
            {
                _courseManager.EditCoursePrograms(id, progamIds);
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
        public IActionResult EditCourseUserRoles([FromForm] int id, [FromForm] int[] userIds, [FromForm] RolesEnum role)
        {
            try
            {
                _courseManager.EditCourseUserRoles(id, userIds, role);
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

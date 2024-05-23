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
        public IActionResult AddCourse([FromForm] string name, [FromForm] string description, [FromForm] int creditPoints)
        {
            try
            {
                var course = new Course(name, description, creditPoints);
                var courseId =  _courseManager.AddCourse(course);

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
        public IActionResult EditCourse([FromForm] int id, [FromForm] string name, [FromForm] string description, [FromForm] int creditPoints)
        {
            try
            {
                var course =  _courseManager.GetCourseById(id);
                if (course == null)
                {
                    var message = $"Course {id} do not exist";
                    _logger.LogError(message);
                    return BadRequest(message);
                }
                course.Name = name;
                course.Description = description;
                course.CreditPoints = creditPoints;
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
        public IActionResult GetCourseTable(int? assignedUserId)
        {
            try
            {
                var courses = _courseManager.GetCourseTableItemList(assignedUserId);
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
        [HttpPost("EditCourseMembers")]
        public IActionResult EditCourseMembers([FromForm] int id, [FromForm] int[] progamIds)
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
                _courseManager.UpdateCourse(course);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Course editing error");
                return BadRequest(e.Message);
            }
        }



    }
}

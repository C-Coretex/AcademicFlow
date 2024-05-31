using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Contracts.Models;
using AcademicFlow.Filters;
using AcademicFlow.Helpers;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Contracts.Models.AssignmentModels.InputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

namespace AcademicFlow.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AssignmentController : Controller
    {
        private readonly IAssignmentManager _assignmentManager;
        private readonly ILogger<AssignmentController> _logger;

        public AssignmentController(IAssignmentManager assignmentManager, ILogger<AssignmentController> logger)
        {
            _assignmentManager = assignmentManager;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);
            var userId = AuthorizationHelpers.GetUserIdIfAuthorized(HttpContext.Session);
            if (userId.HasValue)
                _assignmentManager.UserId = userId.Value;
        }

        // Add assignment task
        [AuthorizeUser(RolesEnum.Professor)]
        [HttpPut("AddAssignmentTask")]
        public async Task<IActionResult> AddAssignmentTask([FromForm] AssignmentTaskInputModel assignmentTask)
        {
            try
            {
                await _assignmentManager.AddAssignmentTask(assignmentTask);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding assignment task");
                return BadRequest(e.Message);
            }
        }

        // Delete assignment task
        [AuthorizeUser(RolesEnum.Professor)]
        [HttpDelete("DeleteAssignmentTask")]
        public async Task<IActionResult> DeleteAssignmentTask([FromQuery] int id)
        {
            try
            {
                await _assignmentManager.DeleteAssignmentTask(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while deleting assignment task");
                return BadRequest(e.Message);
            }
        }

        // Get assignment task
        [AuthorizeUser(RolesEnum.Professor, RolesEnum.Student)]
        [HttpGet("GetAssignmentTask")]
        public async Task<IActionResult> GetAssignmentTask([FromQuery] int id)
        {
            try
            {
                var data = await _assignmentManager.GetAssignmentTask(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting assignment task");
                return BadRequest(e.Message);
            }
        }

        // Add assignment entry (upload file)
        [AuthorizeUser(RolesEnum.Student)]
        [HttpPost("AddAssignmentEntry")]
        public async Task<IActionResult> AddAssignmentEntry([FromForm] int assignmentTaskId, [FromForm] IFormFile file)
        {
            try
            {
                var fileModel = new FileModel()
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType
                };

                if (file.Length > 0)
                {
                    using var ms = new MemoryStream();
                    file.CopyTo(ms);

                    var fileBytes = ms.ToArray();
                    fileModel.Data = fileBytes;
                }

                await _assignmentManager.AddAssignmentEntry(assignmentTaskId, fileModel);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding assignment entry");
                return BadRequest(e.Message);
            }
        }

        // Delete assignment entry
        [AuthorizeUser(RolesEnum.Student)]
        [HttpDelete("DeleteAssignmentEntry")]
        public async Task<IActionResult> DeleteAssignmentEntry([FromQuery] int id)
        {
            try
            {
                await _assignmentManager.DeleteAssignmentEntry(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while deleting assignment entry");
                return BadRequest(e.Message);
            }
        }

        // Get assignment entry
        [AuthorizeUser(RolesEnum.Professor, RolesEnum.Student)]
        [HttpGet("GetAssignmentEntry")]
        public async Task<IActionResult> GetAssignmentEntry([FromQuery] int id)
        {
            try
            {
                var data = await _assignmentManager.GetAssignmentEntry(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting assignment entry");
                return BadRequest(e.Message);
            }
        }

        // Download assignment file
        [AuthorizeUser(RolesEnum.Professor, RolesEnum.Student)]
        [HttpGet("DownloadAssignmentFile")]
        public async Task<IActionResult> DownloadAssignmentFile([FromQuery] int id)
        {
            try
            {
                var data = await _assignmentManager.DownloadAssignmentFile(id);
                return File(data.Data, data.ContentType, data.FileName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while downloading assignment file");
                return BadRequest(e.Message);
            }
        }

        // Add assignment grade
        [AuthorizeUser(RolesEnum.Professor)]
        [HttpPut("AddAssignmentGrade")]
        public async Task<IActionResult> AddAssignmentGrade([FromBody] AssignmentGradeInputModel assignmentGrade)
        {
            try
            {
                await _assignmentManager.AddAssignmentGrade(assignmentGrade);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding assignment grade");
                return BadRequest(e.Message);
            }
        }

        // Delete assignment grade
        [AuthorizeUser(RolesEnum.Professor)]
        [HttpDelete("DeleteAssignmentGrade")]
        public async Task<IActionResult> DeleteAssignmentGrade([FromQuery] int id)
        {
            try
            {
                await _assignmentManager.DeleteAssignmentGrade(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while deleting assignment grade");
                return BadRequest(e.Message);
            }
        }

        // Get assignment grade
        [AuthorizeUser(RolesEnum.Professor, RolesEnum.Student)]
        [HttpGet("GetAssignmentGrade")]
        public async Task<IActionResult> GetAssignmentGrade([FromQuery] int id)
        {
            try
            {
                var data = await _assignmentManager.GetAssignmentGrade(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting assignment grade");
                return BadRequest(e.Message);
            }
        }

        // Get assignment grade
        [AuthorizeUser(RolesEnum.Professor)]
        [HttpGet("GetAssignmentEntriesForAssignmentTask")]
        public async Task<IActionResult> GetAssignmentEntriesForAssignmentTask([FromQuery] int id)
        {
            try
            {
                var data = await _assignmentManager.GetAssignmentEntriesForAssignmentTask(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting assignment grade");
                return BadRequest(e.Message);
            }
        }

        // Get assignment grade
        [AuthorizeUser(RolesEnum.Student)]
        [HttpGet("GetMyAssignmentEntryForAssignmentTask")]
        public async Task<IActionResult> GetMyAssignmentEntryForAssignmentTask([FromQuery] int id)
        {
            try
            {
                var data = await _assignmentManager.GetMyAssignmentEntryForAssignmentTask(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting assignment grade");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser(RolesEnum.Professor, RolesEnum.Student)]
        [HttpGet("GetAllAssignmentsForCourse")]
        public async Task<IActionResult> GetAllAssignmentsForCourse([FromQuery] int courseId, [FromQuery] bool withAssignedEntries = true, [FromQuery] bool withGrades = true, [FromQuery] string dateFrom = null, [FromQuery] string dateTo = null)
        {
            try
            {
                var dateFromDateTime = (DateTime?)null;
                var dateToDateTime = (DateTime?)null;

                if (dateFrom != null)
                {
                    dateFromDateTime = DateTime.ParseExact(dateFrom, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }

                if (dateTo != null)
                {
                    dateToDateTime = DateTime.ParseExact(dateTo, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }

                var data = await _assignmentManager.GetAllAssignmentsForCourse(courseId, withAssignedEntries, withGrades, dateFromDateTime, dateToDateTime);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting all assignments for course");
                return BadRequest(e.Message);
            }
        }

        // Get all assignment grades for all courses
        [AuthorizeUser(RolesEnum.Student)]
        [HttpGet("GetAllAssignmentGradesForAllCourses")]
        public async Task<IActionResult> GetAllAssignmentGradesForAllCourses()
        {
            try
            {
                var data = await _assignmentManager.GetAllAssignmentsForAllCourses();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting all assignment grades for all courses");
                return BadRequest(e.Message);
            }
        }
    }
}
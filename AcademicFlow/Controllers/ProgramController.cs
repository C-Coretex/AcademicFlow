using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Filters;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Managers;
using Microsoft.AspNetCore.Mvc;
namespace AcademicFlow.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProgramController(ILogger<ProgramController> logger, IProgramManager programManager) : Controller
    {
        private readonly ILogger<ProgramController> _logger = logger;
        private readonly IProgramManager _programManager = programManager;

        [AuthorizeUser(RolesEnum.Admin)]
        [HttpPut("AddProgram")]
        public IActionResult AddProgramm([FromForm] string name, [FromForm] int semesterNr)
        {
            try
            {
                var program = new Domain.Contracts.Entities.Program(name, semesterNr);
                var programId = _programManager.AddProgram(program);

                return Ok(programId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Program cretion error");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser(RolesEnum.Admin)]
        [HttpPost("EditProgram")]
        public IActionResult EditProgram([FromForm] int id, [FromForm] string name, [FromForm] int semesterNr)
        {
            try
            {
                var program = _programManager.GetProgramById(id);
                if (program == null)
                {
                    var message = $"Program {id} do not exist";
                    _logger.LogError(message);
                    return BadRequest(message);
                }
                program.Name = name;
                program.SemesterNr = semesterNr;
                _programManager.UpdateProgram(program);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Program editing error");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser(RolesEnum.Admin, RolesEnum.Proffesor, RolesEnum.Student)]
        [HttpGet("GetProgram")]
        public IActionResult GetProgram(int id)
        {
            try
            {
                var program = _programManager.GetProgramById(id);
                return Ok(program);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Program getting error");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser(RolesEnum.Admin, RolesEnum.Proffesor, RolesEnum.Student)]
        [HttpGet("GetProgramTable")]
        public IActionResult GetProgramTable(int? assignedUserId)
        {
            try
            {
                var programs = _programManager.GetProgramTableItemList(assignedUserId);
                return PartialView("Partials/_ProgramTable", programs); /// return html content
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Program table getting error");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser(RolesEnum.Admin)]
        [HttpDelete("DeleteProgram")]
        public IActionResult DeleteProgram([FromForm] int id)
        {
            try
            {
                _programManager.DeleteProgram(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Program delete error");
                return BadRequest(e.Message);
            }
        }

        /// <param name="role">Assing user as professor or as student</param>
        [AuthorizeUser(RolesEnum.Admin)]
        [HttpPost("EditProgramUserRoles")]
        public IActionResult EditProgramUserRoles([FromForm] int id, [FromForm] int[] userIds)
        {
            try
            {
                _programManager.EditProgramUserRoles(id, userIds);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Program editing error");
                return BadRequest(e.Message);
            }
        }

    }
}

using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Filters;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        public async Task<IActionResult> AddProgram([FromForm] string name, [FromForm] int semesterNr)
        {
            try
            {
                var program = new Domain.Contracts.Entities.Program(name, semesterNr);
                var programId = await _programManager.AddProgramAsync(program);

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
        public async Task<IActionResult> EditProgram([FromForm] int id, [FromForm] string name, [FromForm] int semesterNr)
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
                await _programManager.UpdateProgramAsync(program);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Program editing error");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser(RolesEnum.Admin, RolesEnum.Professor, RolesEnum.Student)]
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

        [AuthorizeUser(RolesEnum.Admin, RolesEnum.Professor, RolesEnum.Student)]
        [HttpGet("GetProgramTable")]
        public IActionResult GetProgramTable(int? assignedUserId = null)
        {
            try
            {
                var programs = _programManager.GetProgramTableItemList(assignedUserId);
                return Ok(programs);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Program table getting error");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser(RolesEnum.Admin)]
        [HttpDelete("DeleteProgram")]
        public async Task<IActionResult> DeleteProgram([FromForm] int id)
        {
            try
            {
                await _programManager.DeleteProgramAsync(id);
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
        public async Task<IActionResult> EditProgramUserRoles([FromForm] int id, [FromForm] int[] userIds)
        {
            try
            {
                var response = await _programManager.EditProgramUserRolesAsync(id, userIds);
                if (!response.IsSuccesful)
                {
                    return BadRequest(response.Error);
                }
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Program editing error");
                return BadRequest(e.Message);
            }
        }

        [AuthorizeUser]
        [HttpGet("GetProgramUsers")]
        public IActionResult GetProgramUsers(int programId)
        {
            try
            {
                var programs = _programManager.GetProgramUsers(programId);
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

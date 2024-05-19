﻿using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Filters;
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
        public UserController(IUserManager userManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
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

        [HttpPost("EditUser")]
        public async Task<IActionResult> EditUser([FromForm] int id, [FromForm] string name, [FromForm] string surname, [FromForm] string personalCode, [FromForm] string? email, [FromForm] string? phoneNumber, [FromForm] int? age)
        {
            try
            {
                /// ToDO: add check for id. 
                /// If id == CurrentUser.Id --> okay. User can edit himself.
                /// If CurrentUser has admin role --> okay. Admin can edit any user.
                /// Otherwise reject.
                
                var user = await _userManager.GetUserById(id);
                if (user == null)
                {
                    var message = $"User {id} do not exist";
                    _logger.LogError(message);
                    return BadRequest(message);
                }
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
    }
}

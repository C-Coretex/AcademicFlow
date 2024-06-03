using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Filters;
using AcademicFlow.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AcademicFlow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AuthorizeUser(RolesEnum.Admin)]
        public IActionResult AdminCenter()
        {
            return View();
        }

        [AuthorizeUser(RolesEnum.Professor, RolesEnum.Student)]
        public IActionResult MainPage()
        {
            return View();
        }

        public IActionResult UserRegistration()
        {
            return View();
        }

        public IActionResult UserPasswordReset()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [AuthorizeUser]
        [HttpGet("Home/Courses")]
        public IActionResult Courses()
        {
            return View();
        }

        [AuthorizeUser]
        [HttpGet("Home/Course/{id}")]
        public IActionResult Course(int id)
        {
            ViewData["CourseId"] = id;

            return View();
        }

        [AuthorizeUser]
        [HttpGet("Home/Assignment/New")]
        public IActionResult NewAssignment()
        {
            return View();
        }

        [AuthorizeUser]
        [HttpGet("Home/Assignments")]
        public IActionResult Assignments()
        {
            return View();
        }

        [AuthorizeUser]
        [HttpGet("Home/Assignment/{id}")]
        public IActionResult Assignment(int id)
        {
            ViewData["AssignmentId"] = id;

            return View();
        }

        [AuthorizeUser]
        [HttpGet("Home/AssignmentEntry/New")]
        public IActionResult NewAssignmentEntry()
        {
            return View();
        }

        [AuthorizeUser]
        [HttpGet("Home/Assignment/{assignmentId}/AssignmentEntry/{id}")]
        public IActionResult AssignmentEntry(int assignmentId, int id)
        {
            ViewData["AssignmentId"] = assignmentId;
            ViewData["AssignmentEntryId"] = id;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

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

        public IActionResult AdminCenter()
        {
            return View();
        }

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
        
        [HttpGet("Home/Courses")]
        public IActionResult Courses()
        {
            return View();
        }

        [HttpGet("Home/Course/{id}")]
        public IActionResult Course(int id)
        {
            ViewData["CourseId"] = id;

            return View();
        }

        [HttpGet("Home/Assignments")]
        public IActionResult Assignments()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebAppDbContext _context;

        // Inject the DbContext
        public HomeController(WebAppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signup()
        {
            var countries = _context.Countries.ToList(); // assuming you're using EF Core
            ViewBag.Countries = countries;
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignupSuccessful()
        {
            return View();
        }

        public IActionResult LoginSuccessful()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
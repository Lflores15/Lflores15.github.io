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
            // Get the list of countries from the database
            var countries = _context.Countries.ToList();

            // Pass the list to the view using ViewBag
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
    }
}
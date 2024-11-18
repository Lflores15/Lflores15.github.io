using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebAppDbContext _context;

        public HomeController(WebAppDbContext context)
        {
            _context = context;
        }

        // Index page (content page with title and buttons)
        public IActionResult Index()
        {
            return View();
        }

        // Signup page
        public IActionResult Signup()
        {
            ViewBag.Countries = _context.Countries.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(User user)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "This email is already in use.");
                    ViewBag.Countries = _context.Countries.ToList();
                    return View(user);
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("SignupSuccessful");
            }

            ViewBag.Countries = _context.Countries.ToList();
            return View(user);
        }

        // Signup successful page
        public IActionResult SignupSuccessful()
        {
            return View();
        }

        // Login page
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                return RedirectToAction("LoginSuccessful", new { firstName = user.FirstName, lastName = user.LastName });
            }

            ModelState.AddModelError("InvalidLogin", "Invalid email or password.");
            return View();
        }

        // Successful login page
       // Successful login page
        public IActionResult LoginSuccessful(string firstName, string lastName)
        {
            ViewBag.FirstName = firstName;
            ViewBag.LastName = lastName;
            return View(); 
        }
    }
}

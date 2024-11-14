using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly WebAppDbContext _context;
    private readonly ILogger<AccountController> _logger;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AccountController(WebAppDbContext context, ILogger<AccountController> logger, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _logger = logger;
        _passwordHasher = passwordHasher;
    }

    // GET: Signup
    public IActionResult Signup()
    {
        ViewBag.Countries = _context.Countries.ToList();
        return View();
    }

    // POST: Signup
    [HttpPost]
    public async Task<IActionResult> Signup(User model)
    {
        if (!ModelState.IsValid)
        {
            foreach (var modelStateKey in ModelState.Keys)
            {
                var value = ModelState[modelStateKey];
                foreach (var error in value.Errors)
                {
                    _logger.LogError($"Validation error in {modelStateKey}: {error.ErrorMessage}");
                }
            }

            ViewBag.Countries = _context.Countries.ToList();
            return View(model);
        }

        if (_context.Users.Any(u => u.Email == model.Email))
        {
            ModelState.AddModelError("Email", "This email is already registered.");
            ViewBag.Countries = _context.Countries.ToList();
            return View(model);
        }

        try
        {
            var hashedPassword = _passwordHasher.HashPassword(model, model.Password);

            var newUser = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CountryId = model.CountryId,
                Password = hashedPassword // Store hashed password
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"New user signed up: {model.Email}");
            return RedirectToAction("SignupSuccessful", "Account");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error saving user to database: {ex.Message}");
            ViewData["DatabaseError"] = "An error occurred while saving your information. Please try again.";
            ViewBag.Countries = _context.Countries.ToList();
            return View(model);
        }
    }

    // GET: SignupSuccessful
    public IActionResult SignupSuccessful()
    {
        return View();
    }

    // GET: Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: Login
    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            ViewData["LoginError"] = "Invalid email or password.";
            return View();
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
        if (result == PasswordVerificationResult.Failed)
        {
            ViewData["LoginError"] = "Invalid email or password.";
            return View();
        }

        // Authentication successful
        // You can implement session/cookie-based login here
        _logger.LogInformation($"User logged in: {email}");

        // Redirect to a protected area, like the homepage or user dashboard
        return RedirectToAction("Index", "Home");
    }
}

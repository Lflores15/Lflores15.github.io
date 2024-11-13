using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;

public class AccountController : Controller
{
    private readonly WebAppDbContext _context;
    private readonly ILogger<AccountController> _logger;

    public AccountController(WebAppDbContext context, ILogger<AccountController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: Signup
    public IActionResult Signup()
    {
        // Pass the list of countries to the view
        ViewBag.Countries = _context.Countries.ToList();
        return View();
    }

    // POST: Signup
    [HttpPost]
    public async Task<IActionResult> Signup(User model)
    {
        // Step 1: Check if model is valid
        if (ModelState.IsValid)
        {
            // Step 2: Password validation
            if (model.Password != model.ConfirmPassword)
            {
                // Password mismatch error
                ViewData["ConfirmPasswordError"] = "Passwords do not match.";
                ViewBag.Countries = await _context.Countries.ToListAsync();
                return View(model);
            }

            // Step 3: Check if email already exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (existingUser != null)
            {
                // Email exists error
                ViewData["EmailError"] = "Email is already taken.";
                ViewBag.Countries = await _context.Countries.ToListAsync();
                return View(model);
            }

            // Step 4: Create a new user
            var newUser = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password, // In a real app, hash the password before saving
                CountryId = model.CountryId
            };

            try
            {
                // Step 5: Save the new user to the database
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                // Step 6: Log the successful signup
                _logger.LogInformation($"New user signed up: {model.Email}");

                // Step 7: Redirect to the Login page
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving user to database: {ex.Message}");
                ViewData["DatabaseError"] = "There was an error saving your information.";
                ViewBag.Countries = await _context.Countries.ToListAsync();
                return View(model);
            }
        }

        // Log validation errors if the model is not valid
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            _logger.LogWarning("ModelState Error: " + error.ErrorMessage);
        }

        // If validation fails, pass countries back to the view
        ViewBag.Countries = await _context.Countries.ToListAsync();
        return View(model);
    }
}

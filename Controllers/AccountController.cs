using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Models;

namespace MovieCatalog.Controllers
{
    // This controller handles user registration
    public class AccountController : Controller
    {
        private readonly UserManager<MyUser> _userManager;

        // Constructor: initializes UserManager for working with users
        public AccountController(UserManager<MyUser> userManager)
        {
            _userManager = userManager;
        }

        // Shows the registration form
        [HttpGet]
        public IActionResult Register(string returnUrl = "/")
        {
            ViewData["ReturnUrl"] = returnUrl; // Save the URL to redirect after success
            return View(new RegisterDTO()); // Return the registration view with an empty model
        }

        // Processes the data submitted from the registration form
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model, string returnUrl = "/")
        {
            // If the model is invalid (missing data or errors), return to the view
            if (!ModelState.IsValid)
                return View(model);

            // Check if the email is already registered
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "The email is already in use."); // Add an error if it exists
                return View(model); // Return to the view with the error
            }

            // Create a new user with the provided data
            var user = new MyUser
            {
                UserName = model.Email, // Use the email as the username
                Email = model.Email
            };

            // Try to register the user in the database
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // If successful, redirect the user
                return LocalRedirect(returnUrl);
            }

            // If there were errors, show them in the view
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model); // Return to the view if registration fails
        }
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Models;
using System.Threading.Tasks;

namespace MovieCatalog.Controllers
{
    // The controller manages user login, logout, and redirection logic.
    [Route("account")] // Base route for the controller
    public class LoginController : Controller
    {
        private readonly SignInManager<MyUser> _signInManager;

        // Constructor to inject the SignInManager for handling user sign-ins and sign-outs.
        public LoginController(SignInManager<MyUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // Handles GET requests to the login page.
        [HttpGet("login")] 
        public IActionResult Login(string returnUrl = "/")
        {
            ViewData["ReturnUrl"] = returnUrl; // Pass the URL to redirect after successful login.
            return View("~/Views/Account/Login.cshtml", new LoginDTO()); // Return the Login view.
        }

        // Handles POST requests for user login attempts.
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model, string returnUrl = "/")
        {
            // Check if the data provided by the user is valid.
            if (!ModelState.IsValid)
                return View("~/Views/Account/Login.cshtml", new LoginDTO());

            // Attempt to sign in using the provided email and password.
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

            if (result.Succeeded) // If login is successful
            {
                return LocalRedirect(returnUrl); // Redirect to the specified return URL.
            }

            // Handle specific login errors, like account lockout or invalid credentials.
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Account is locked out."); // Show a lockout error message.
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt."); // Show a generic error for invalid login.
            }

            // Return to the login page if there was an error.
            return View("~/Views/Account/Login.cshtml", new LoginDTO());
        }

        // Handles POST requests to log the user out.
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); // Log the user out of the session.
            return RedirectToAction("Index", "Home"); // Redirect to the home page after logout.
        }
    }
}

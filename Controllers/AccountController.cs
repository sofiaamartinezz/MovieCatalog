using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Models;

namespace MovieCatalog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<MyUser> _userManager;

        // Constructor to inject the UserManager service
        public AccountController(UserManager<MyUser> userManager)
        {
            _userManager = userManager;
        }

        // POST: api/account/register
        // Registers a new user by processing their details (email, password).
        // Input: RegisterDTO (JSON format).
        // Output: Success message or error details.
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            // Check if the provided data is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 Bad Request with validation errors
            }

            // Verify if the email is already in use
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return BadRequest("The email is already in use."); // Return 400 Bad Request
            }

            // Create a new user with the provided data
            var user = new MyUser
            {
                UserName = model.Email, // Use email as username
                Email = model.Email
            };

            // Attempt to create the user in the database
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok("User registered successfully."); // Return 200 OK
            }

            // If registration failed, return the errors
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState); // Return 400 Bad Request with errors
        }
    }
}

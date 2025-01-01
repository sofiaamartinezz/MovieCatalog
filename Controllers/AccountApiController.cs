using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Models;
using System.Threading.Tasks;

namespace MovieCatalog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountApiController : ControllerBase
    {
        private readonly UserManager<MyUser> _userManager;

        public AccountApiController(UserManager<MyUser> userManager)
        {
            _userManager = userManager;
        }

        // POST: api/account/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            // Check if the model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if the email is already registered
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "The email is already in use." });
            }

            // Create a new user object
            var user = new MyUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            // Attempt to create the user
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "User registered successfully." });
            }

            // If there were errors, return them in the response
            return BadRequest(new { message = "Registration failed", errors = result.Errors });
        }
    }
}

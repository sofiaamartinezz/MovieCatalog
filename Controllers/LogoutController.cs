using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieCatalog.Models;
using System.Threading.Tasks;

namespace MovieCatalog.Controllers
{
    public class LogoutController : Controller
    {
        private readonly SignInManager<MyUser> _signInManager;
        private readonly ILogger<LogoutController> _logger;

        public LogoutController(SignInManager<MyUser> signInManager, ILogger<LogoutController> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            
            return RedirectToAction("Index", "Home");
        }

        // API endpoint for programmatic logout
        [HttpPost("api/logout")]
        public async Task<IActionResult> ApiLogout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out via API.");
            return Ok(new { message = "Logged out successfully" });
        }
    }
}
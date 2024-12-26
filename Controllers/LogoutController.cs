using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Models;
using System.Threading.Tasks;

namespace MovieCatalog.Controllers
{
    // This controller is responsible for handling user logout actions.  
    public class LogoutController : Controller
    {
        private readonly SignInManager<MyUser> _signInManager;

        // Constructor to inject the SignInManager, which is used to manage user authentication actions.
        public LogoutController(SignInManager<MyUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // Handles the logout action when triggered via a POST request.
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            // Signs the user out of the current session.
            await _signInManager.SignOutAsync();

            // Redirects the user to the home page after logging out.
            return RedirectToAction("Index", "Home");
        }
    }
}

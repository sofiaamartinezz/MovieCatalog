using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Models;

namespace MovieCatalog.Controllers
{
    public class LogoutController : Controller
    {
        private readonly SignInManager<MyUser> _signInManager;

        public LogoutController(SignInManager<MyUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); // Redirige a la página de inicio
        }
    }
}

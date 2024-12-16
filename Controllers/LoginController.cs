using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Models;
using System.Threading.Tasks;

namespace MovieCatalog.Controllers
{
    [Route("account")]
    public class LoginController : Controller
    {
        private readonly SignInManager<MyUser> _signInManager;

        public LoginController(SignInManager<MyUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl = "/")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View("~/Views/Account/Login.cshtml", new LoginDTO());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model, string returnUrl = "/")
        {
            if (!ModelState.IsValid)
                return View("~/Views/Account/Login.cshtml", new LoginDTO());

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Account is locked out.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View("~/Views/Account/Login.cshtml", new LoginDTO());
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

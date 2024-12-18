using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Models;

namespace MovieCatalog.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<MyUser> _userManager;

        public AccountController(UserManager<MyUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = "/")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new RegisterDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model, string returnUrl = "/")
        {
            if (!ModelState.IsValid)
                return View(model);

            // Verificar si el correo ya existe en la base de datos
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "The email is already in use.");
                return View(model);
            }

            var user = new MyUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        // Método auxiliar para validar la contraseña
        private bool IsPasswordValid(string password)
        {
            if (password.Length < 8) return false;

            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasNumber = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpperCase && hasNumber && hasSpecialChar;
        }
    }
}

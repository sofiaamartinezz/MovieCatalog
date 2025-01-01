using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Models;
using System.Threading.Tasks;

namespace MovieCatalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogoutController : ControllerBase
{
    private readonly SignInManager<MyUser> _signInManager;

    // Constructor: Injects the SignInManager, which manages user authentication actions.
    public LogoutController(SignInManager<MyUser> signInManager)
    {
        _signInManager = signInManager;
    }

    // POST: api/logout
    // Logs out the currently authenticated user and ends their session.
    [HttpPost]
    public async Task<IActionResult> Index()
    {
        // Signs the user out of their current session.
        await _signInManager.SignOutAsync();

        // Returns a confirmation message after logging out.
         return RedirectToAction("Index", "Home");
    }
}





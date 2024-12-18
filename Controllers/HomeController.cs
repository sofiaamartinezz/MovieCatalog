using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Models;
using MovieCatalog.Data;


namespace MovieCatalog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public IActionResult Privacy()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtener el ID del usuario logueado

        // Obtener solo las películas asociadas al usuario actual
        var movies = _context.UserMovies
            .Where(um => um.UserId == userId)
            .Select(um => um.Movie)
            .ToList();

        return View(movies); // Enviar las películas asociadas al usuario a la vista
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

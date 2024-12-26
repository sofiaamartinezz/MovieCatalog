using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Models;
using MovieCatalog.Data;

namespace MovieCatalog.Controllers;

// This controller handles the main actions for the app, like loading the homepage and user-specific data
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger; // Logger to track application events
    private readonly ApplicationDbContext _context; // Database context to access data

    // Constructor: sets up the logger and database context
    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    // Default action for the homepage
    public IActionResult Index()
    {
        return View(); // Just returns the homepage view
    }

    // Action for the "Privacy" page, which is protected (only logged-in users can access)
    [Authorize]
    public IActionResult Privacy()
    {
        // Get the logged-in user's ID from their claims
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Get movies associated with the logged-in user
        var movies = _context.UserMovies
            .Where(um => um.UserId == userId) // Filter for the current user's data
            .Select(um => um.Movie) // Get the related movie info
            .ToList();

        return View(movies); // Pass the movies to the view
    }

    // Error handling
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        // Creates an error model with the current request's ID and shows the error view
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Data;
using MovieCatalog.Models;

namespace MovieCatalog.Controllers
{
    //[Route("movies")] // Base route for the controller 
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the database context.
        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        // Displays a list of movies associated with the logged-in user.
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Check if the user is authenticated (logged in).
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page if the user is not logged in.
                return RedirectToAction("Login", "Account");
            }

            // Get the logged-in user's ID.
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Retrieve movies associated with the current user.
            var movies = await _context.UserMovies
                .Where(um => um.UserId == userId)
                .Select(um => um.Movie)
                .ToListAsync();

            // Pass the movies to the view.
            return View(movies);
        }

        // GET: Movies/Details/5
        // Displays details for a specific movie by ID.
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                // If no ID is provided, show a 404 error.
                return NotFound();
            }

            // Look for the movie in the database.
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                // If the movie isn't found, show a 404 error.
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        // Shows the form to create a new movie.
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // Handles the form submission for creating a new movie.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Title,Genre,Director,Rating")] Movie movie)
        {
            if (ModelState.IsValid) // Checks if the form input is valid.
            {
                // Add the new movie to the database.
                _context.Add(movie);
                await _context.SaveChangesAsync();

                // Associate the movie with the logged-in user.
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var userMovie = new UserMovie
                {
                    UserId = userId,
                    MovieId = movie.MovieId
                };

                _context.UserMovies.Add(userMovie);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index)); // Redirect back to the movie list.
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        // Shows the form to edit an existing movie.
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Find the movie by its ID.
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // Handles form submission for editing a movie.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Title,Genre,Director,Rating")] Movie movie)
        {
            if (id != movie.MovieId)
            {
                // If the movie IDs don't match, show a 404 error.
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update the movie in the database.
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle the case where the movie no longer exists.
                    if (!MovieExists(movie.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw; // Re-throw the exception for debugging purposes.
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the movie list.
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieId == id);
                
            if (movie == null)
            {
                return NotFound();
            }

            // Verify the user has access to this movie
            var hasAccess = await _context.UserMovies
                .AnyAsync(um => um.MovieId == id && um.UserId == userId);
                
            if (!hasAccess)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Login", "Account");
                }

                // First, verify the movie exists and the user has access to it
                var userMovie = await _context.UserMovies
                    .FirstOrDefaultAsync(um => um.MovieId == id && um.UserId == userId);

                if (userMovie == null)
                {
                    TempData["Error"] = "Movie not found or you don't have permission to delete it.";
                    return RedirectToAction(nameof(Index));
                }

                // Remove the UserMovie association
                _context.UserMovies.Remove(userMovie);

                // Check if this movie is used by other users
                var otherUsersHaveThisMovie = await _context.UserMovies
                    .AnyAsync(um => um.MovieId == id && um.UserId != userId);

                if (!otherUsersHaveThisMovie)
                {
                    // If no other users have this movie, delete the movie itself
                    var movie = await _context.Movies.FindAsync(id);
                    if (movie != null)
                    {
                        _context.Movies.Remove(movie);
                    }
                }

                await _context.SaveChangesAsync();
                TempData["Success"] = "Movie successfully deleted.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the movie.";
            }

            return RedirectToAction(nameof(Index));
        }

                // Checks if a movie exists in the database by its ID.
                private bool MovieExists(int id)
                {
                    return _context.Movies.Any(e => e.MovieId == id);
                }
    }
}
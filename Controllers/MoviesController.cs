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
        [ValidateAntiForgeryToken] // Helps prevent cross-site request forgery attacks.
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
        // Shows a confirmation page for deleting a movie.
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Look for the movie in the database.
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        // Handles the form submission for deleting a movie.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Find the movie by its ID.
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                // Remove the associations in UserMovies.
                var userMovies = _context.UserMovies.Where(um => um.MovieId == id);
                _context.UserMovies.RemoveRange(userMovies);

                // Delete the movie from the database.
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync(); // Commit the changes to the database.
            return RedirectToAction(nameof(Index));
        }

        // Checks if a movie exists in the database by its ID.
        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }
    }
}
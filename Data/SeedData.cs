using MovieCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieCatalog.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Check if there's already data
            if (context.Movies.Any())
            {
                return; // if there's data, we do nothing
            }

            // Initial Data
            context.Movies.AddRange(
                new Movie
                {
                    Title = "Inception",
                    Genre = "Sci-Fi",
                    Director = "Christopher Nolan",
                    Rating = "9.0"
                },
                new Movie
                {
                    Title = "The Shawshank Redemption",
                    Genre = "Drama",
                    Director = "Frank Darabont",
                    Rating = "9.3"
                },
                new Movie
                {
                    Title = "The Godfather",
                    Genre = "Crime",
                    Director = "Francis Ford Coppola",
                    Rating = "9.2"
                }
            );

            context.SaveChanges(); // Save in the database
        }
    }
}

using System;
using MovieCatalog.Models;
using System.ComponentModel.DataAnnotations;


namespace MovieCatalog.Models;
public class Movie
{
    public int MovieId { get; set; }
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public string? Director { get; set; }

    [Range(0.0, 5.0, ErrorMessage = "Rating must be between 0 and 5.")]
    public double? Rating { get; set; }

    // Propiedad de navegación para la relación con UserMovie
    public ICollection<UserMovie> UserMovies { get; set; } = new List<UserMovie>();
}


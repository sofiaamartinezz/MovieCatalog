using System;
using MovieCatalog.Models;
public class Movie
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public string Description { get; set; }

    // Propiedad de navegación para la relación con UserMovie
    public ICollection<UserMovie> UserMovies { get; set; }
}

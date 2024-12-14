using System;
using MovieCatalog.Models;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    // Propiedad de navegación para la relación con UserMovie
    public ICollection<UserMovie> UserMovies { get; set; }
}

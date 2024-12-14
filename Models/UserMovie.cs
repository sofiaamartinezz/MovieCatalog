using System;
using MovieCatalog.Models;

public class UserMovie
{
    public int UserId { get; set; }
    public User User { get; set; }  // Relación con el modelo User

    public int MovieId { get; set; }
    public Movie Movie { get; set; }  // Relación con el modelo Movie
}

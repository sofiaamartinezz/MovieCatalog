using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using MovieCatalog.Models;

namespace MovieCatalog.Identity
{
    public class MyUser : IdentityUser
    {
        // Propiedades adicionales para el dominio
        public string? FullName { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<UserMovie>? UserMovies { get; set; }  // Relación con UserMovie
    }
}

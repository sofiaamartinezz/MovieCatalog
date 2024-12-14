using Microsoft.EntityFrameworkCore;
using MovieCatalog.Models;  // Asegúrate de que el namespace sea correcto

namespace MovieCatalog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }  // DbSet para la tabla de Movies
        public DbSet<User> Users { get; set; }    // DbSet para la tabla de Users
    }
}

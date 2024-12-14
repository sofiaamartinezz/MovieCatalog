using Microsoft.EntityFrameworkCore;
using MovieCatalog.Models;  // Asegúrate de que el namespace sea correcto

namespace MovieCatalog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }  // Agregar el DbSet para UserMovie

        // Constructor con DbContextOptions
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la relación muchos a muchos entre User y Movie
            modelBuilder.Entity<UserMovie>()
                .HasKey(um => new { um.UserId, um.MovieId });  // La clave primaria es la combinación de UserId y MovieId

            modelBuilder.Entity<UserMovie>()
                .HasOne(um => um.User)
                .WithMany(u => u.UserMovies)
                .HasForeignKey(um => um.UserId);

            modelBuilder.Entity<UserMovie>()
                .HasOne(um => um.Movie)
                .WithMany(m => m.UserMovies)
                .HasForeignKey(um => um.MovieId);
        }
    }
}

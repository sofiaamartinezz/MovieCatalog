using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MovieCatalog.Models;

namespace MovieCatalog.Data
{
    public class ApplicationDbContext : IdentityDbContext<MyUser, MyRol, string>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la relación entre UserMovie y Movie
            modelBuilder.Entity<UserMovie>()
                .HasKey(um => new { um.UserId, um.MovieId });

            modelBuilder.Entity<UserMovie>()
                .HasOne(um => um.User)
                .WithMany(m => m.UserMovies)
                .HasForeignKey(um => um.UserId);

            modelBuilder.Entity<UserMovie>()
                .HasOne(um => um.Movie)
                .WithMany(m => m.UserMovies)
                .HasForeignKey(um => um.MovieId);
        }
    }
}

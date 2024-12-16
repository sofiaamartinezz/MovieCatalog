using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Models;

namespace MovieCatalog.Identity;

public class MyIdentityDBContext : IdentityDbContext<MyUser, MyRol, String>
{
    public MyIdentityDBContext(DbContextOptions<MyIdentityDBContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<UserMovie>()
        .HasKey(um => new { um.UserId, um.MovieId });

    modelBuilder.Entity<UserMovie>()
        .HasOne(um => um.User)
        .WithMany(u => u.UserMovies)
        .HasForeignKey(um => um.UserId);

    modelBuilder.Entity<UserMovie>()
        .HasOne(um => um.Movie)
        .WithMany(m => m.UserMovies)
        .HasForeignKey(um => um.MovieId);
    }    

};




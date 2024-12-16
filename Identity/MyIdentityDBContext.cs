using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Models;

namespace MovieCatalog.Identity
{
    public class MyIdentityDBContext : IdentityDbContext<MyUser, MyRol, string>
    {
        public MyIdentityDBContext(DbContextOptions<MyIdentityDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<UserMovie>();
        }
    }
}

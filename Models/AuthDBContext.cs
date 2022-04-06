using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace rapid_news_media_auth_api.Models
{
    public class AuthDBContext : DbContext
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                   Id = 1,
                   Firstname = "Sergiu",
                   Lastname = "Dobrovolschi",
                   Username = "sergiu@email.com"
                }
            );
        }

    }
}
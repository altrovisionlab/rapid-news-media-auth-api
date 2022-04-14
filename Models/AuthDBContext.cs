using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace rapid_news_media_auth_api.Models
{
    public class AuthDBContext : DbContext
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;

        //Data seeding
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User {Id = 1, Firstname = "Sergiu", Lastname = "Dobrovolschi", Username = "sergiu@email.com"},
                new User {Id = 2, Firstname = "Mark", Lastname = "Goel", Username = "mark@email.com"},
                new User {Id = 3, Firstname = "Rachel", Lastname = "Fetcher", Username = "sina@email.com"},
                new User {Id = 4, Firstname = "Derek", Lastname = "Lotira", Username = "derek@email.com"}
                );
        }

    }
}
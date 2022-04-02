using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace rapid_news_media_auth_api.Models
{
    public class AuthDBContext : DbContext
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> options): base(options) {}

        public DbSet<User> Users {get; set;} = null!;

    }
}
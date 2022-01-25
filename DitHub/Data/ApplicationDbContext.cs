using DitHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DitHub.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Dit> Dits => Set<Dit>();
        public DbSet<Genre> Genres => Set<Genre>();
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

    }
}

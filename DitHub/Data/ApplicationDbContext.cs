using DitHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DitHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Dit> Dits => Set<Dit>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<FaveDit> FaveDits => Set<FaveDit>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FaveDit>()
                        .HasKey(f => new { f.DitId, f.AppUserId });

            modelBuilder.Entity<FaveDit>()
                        .HasOne(f => f.Dit)
                        .WithMany(d => d.FaveDits)
                        .HasForeignKey(f => f.DitId)
                        .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<FaveDit>()
                        .HasOne(f => f.AppUser)
                        .WithMany(a => a.FaveDits)
                        .HasForeignKey(f => f.AppUserId)
                        .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}

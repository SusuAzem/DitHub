using DitHub.Core.Models;
using DitHub.Persistence.EntityConfigurations;
using DitHub.Persistence.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DitHub.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>, IApplicationDbContext
    {
        //public DbSet<AppUser> Users { get; set; } = null!;
        public DbSet<Dit> Dits { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<FaveDit> FaveDits { get; set; } = null!;
        public DbSet<Following> Followings { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<UserNotification> UserNotifications { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new DitConfiguration().Configure(modelBuilder.Entity<Dit>());
            new AppUserConfiguration().Configure(modelBuilder.Entity<AppUser>());
            new FaveDitConfiguration().Configure(modelBuilder.Entity<FaveDit>());
            new FollowingConfiguration().Configure(modelBuilder.Entity<Following>());
            new GenreConfiguration().Configure(modelBuilder.Entity<Genre>());
            new UserNotificationConfiguration().Configure(modelBuilder.Entity<UserNotification>());
        }

        public void Add(Dit dit)
        {
            throw new System.NotImplementedException();
        }
    }
}

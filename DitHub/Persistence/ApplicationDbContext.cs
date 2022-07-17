using DitHub.Core.Models;
using DitHub.Persistence.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DitHub.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Dit> Dits => Set<Dit>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<FaveDit> FaveDits => Set<FaveDit>();
        public DbSet<Following> Followings => Set<Following>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<UserNotification> UserNotifications => Set<UserNotification>();

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
    }
}

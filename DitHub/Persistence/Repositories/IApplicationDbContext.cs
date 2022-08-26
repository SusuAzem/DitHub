using DitHub.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DitHub.Persistence.Repositories
{
    public interface IApplicationDbContext
    {
        DbSet<AppUser> Users { get; set; }
        DbSet<Dit> Dits { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<FaveDit> FaveDits { get; set; }
        DbSet<Following> Followings { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<UserNotification> UserNotifications { get; set; }

        void Add(Dit dit);
    }
}
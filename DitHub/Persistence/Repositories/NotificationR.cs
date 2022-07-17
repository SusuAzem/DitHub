using DitHub.Core.IRepositories;
using DitHub.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DitHub.Persistence.Repositories
{
    public class NotificationR : INotificationR
    {
        private readonly ApplicationDbContext dbContext;

        public NotificationR(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Notification> Get3OldNotifi(string id)
        {

            return dbContext.Notifications
            .Include(n => n.Dit.AppUser)
            .Where(n => n.UserNotifications!
            .Where(un => un.AppUserId == id).Any())
            .OrderByDescending(n => n.DateTime)
            .Take(3)
            .ToList();

        }

        public IEnumerable<Notification> GetNewNotifi(string id)
        {

            return dbContext.Notifications
                .Include(n => n.Dit.AppUser)
                .Where(n => n.UserNotifications!
                .Where(un => un.AppUserId == id && !un.IsRead).Any())
                .ToList();

        }

    }
}

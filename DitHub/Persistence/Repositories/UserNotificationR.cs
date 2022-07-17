using DitHub.Core.IRepositories;
using DitHub.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace DitHub.Persistence.Repositories
{
    public class UserNotificationR : IUserNotificationR
    {
        private readonly ApplicationDbContext dbContext;

        public UserNotificationR(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<UserNotification> GetUserNewNotifi(string id)
        {

            return dbContext.UserNotifications
               .Where(un => un.AppUserId == id && !un.IsRead)
               .ToList();

        }
    }
}

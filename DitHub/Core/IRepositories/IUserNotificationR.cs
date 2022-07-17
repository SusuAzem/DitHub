using DitHub.Core.Models;
using System.Collections.Generic;

namespace DitHub.Core.IRepositories
{
    public interface IUserNotificationR
    {
        IEnumerable<UserNotification> GetUserNewNotifi(string id);
    }
}
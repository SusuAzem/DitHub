using DitHub.Core.Models;
using System.Collections.Generic;

namespace DitHub.Core.IRepositories
{
    public interface INotificationR
    {
        IEnumerable<Notification> GetNewNotifi(string id);
        IEnumerable<Notification> Get3OldNotifi(string id);
    }
}
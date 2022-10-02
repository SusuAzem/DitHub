using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DitHub.Core.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string? Photo { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        [JsonIgnore]
        public virtual ICollection<Dit>? Dits { get; private set; }
        [JsonIgnore]
        public virtual ICollection<FaveDit>? FaveDits { get; private set; }
        [JsonIgnore]
        public virtual ICollection<Following>? Followers { get; private set; }
        [JsonIgnore]
        public virtual ICollection<Following>? Followees { get; private set; }
        [JsonIgnore]
        public virtual ICollection<UserNotification>? UserNotifications { get; private set; }


        internal void Notifi(Notification notification)
        {
            UserNotifications!.Add(new UserNotification(this, notification));
        }

        public AppUser()
        {
            UserNotifications = new List<UserNotification>();
            Dits = new List<Dit>();
            FaveDits = new List<FaveDit>();
            Followers = new List<Following>();
            Followees = new List<Following>();
        }
    }
}

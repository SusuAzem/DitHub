using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DitHub.Core.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string? Photo { get; set; }
        [JsonIgnore]
        public virtual ICollection<Dit>? Dits { get; set; }
        [JsonIgnore]
        public virtual ICollection<FaveDit>? FaveDits { get; set; }
        [JsonIgnore]
        public virtual ICollection<Following>? Followers { get; set; }
        [JsonIgnore]
        public virtual ICollection<Following>? Followees { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserNotification>? UserNotifications { get; set; }


        internal void Notifi(Notification notification)
        {
            UserNotifications!.Add(new UserNotification(this, notification));
        }
    }
}

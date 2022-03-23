using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DitHub.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        public string? Photo { get; set; }
        public virtual ICollection<Dit>? Dits { get; set; }
        public virtual ICollection<FaveDit>? FaveDits { get; set; }
        public virtual ICollection<Following>? Followers { get; set; }
        public virtual ICollection<Following>? Followees { get; set; }
        public virtual ICollection<UserNotification>? UserNotifications { get; set; }


        internal void Notifi(Notification notification)
        {
            UserNotifications!.Add(new UserNotification(this, notification));
        }
    }
}

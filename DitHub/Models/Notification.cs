using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DitHub.Models
{
    public class Notification
    {
        protected Notification()
        {
        }

        public int Id { get; set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? DateTime0 { get; private set; }
        public string? Venue0 { get; private set; }
        [Required]
        [ForeignKey("Dit")]
        public int DitId { get; set; }
        public virtual Dit Dit { get; private set; } = null!;
        public virtual ICollection<UserNotification>? UserNotifications { get; set; }

        private Notification(NotificationType type, Dit dit)
        {
            Dit = dit;
            Type = type;
            DateTime = DateTime.Now;
        }

        public static Notification DitCreated(Dit dit)
        {
            return new Notification(NotificationType.DitCreated, dit);
        }

        public static Notification DitUpdated(Dit dit, DateTime dateTime, string venue)
        {
            var notifi = new Notification(NotificationType.DitUpdated, dit);

            notifi.DateTime0 = dateTime;
            notifi.Venue0 = venue;

            return notifi;
        }

        public static Notification DitCanceled(Dit dit)
        {
            return new Notification(NotificationType.DitCanceled, dit);
        }


    }
}

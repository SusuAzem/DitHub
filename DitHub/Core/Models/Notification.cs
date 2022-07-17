using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DitHub.Core.Models
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
        public string? Changed { get; private set; }
        public int DitId { get; set; }
        public virtual Dit Dit { get; private set; } = null!;
        [JsonIgnore]
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

        public static Notification DitUpdated(Dit dit, DateTime dateTime, string venue, string changed)
        {
            var notifi = new Notification(NotificationType.DitUpdated, dit)
            {
                DateTime0 = dateTime,
                Venue0 = venue,
                Changed = changed
            };
            return notifi;
        }

        public static Notification DitCanceled(Dit dit)
        {
            return new Notification(NotificationType.DitCanceled, dit);
        }


    }
}

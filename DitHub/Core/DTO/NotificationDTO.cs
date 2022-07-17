using DitHub.Core.Models;
using System;

namespace DitHub.Core.DTO
{
    public class NotificationDTO
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? DateTime0 { get; set; }
        public string? Venue0 { get; set; }

        public string? Changed { get; set; }

        public DitDTO Dit { get; set; } = null!;

        public string? Statue { get; set; }
    }
}
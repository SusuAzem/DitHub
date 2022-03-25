using DitHub.Models;
using System;

namespace DitHub.DTO
{
    public class NotificationDTO
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? DateTime0 { get; set; }
        public string? Venue0 { get; set; }
        public DitDTO Dit { get; set; } = null!;
    }
}
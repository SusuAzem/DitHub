using DitHub.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace DitHub.Models
{
    public class Dit
    {
        public Dit()
        {
        }
        public Dit(string id, DateTime dateTime, byte genre, string venue)
        {
            AppUserId = id;
            Date = dateTime;
            GenreId = genre;
            Venue = venue;
        }

        public int Id { get; private set; }
        public virtual AppUser AppUser { get; private set; } = null!;
        [Required]
        [ForeignKey("AppUser")]
        public string AppUserId { get; private set; } = null!;
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(255)]
        public string Venue { get; set; } = "";
        public virtual Genre Genre { get; set; } = null!;
        [Required]
        [ForeignKey("Genre")]
        public byte GenreId { get; set; }
        [JsonIgnore]
        public virtual ICollection<FaveDit>? FaveDits { get; private set; }
        public bool RemoveFlag { get; private set; }
        [JsonIgnore]
        public virtual ICollection<Notification>? Notifications { get; private set; }
        internal void Remove()
        {
            RemoveFlag = true;

            var notifi = Notification.DitCanceled(this);

            foreach (var item in FaveDits!.Select(f => f.AppUser))
            {
                item.Notifi(notifi);
            }
        }

        internal void Update(CreateViewModel viewModel)
        {
            Notification notifi;
            string changed = "none";
            //saving old value
            if (Date != viewModel.GetDateTime())
            {
                changed = "date";
            }
            if (Venue != viewModel.Venue)
            {
                changed = "venue";
            }
            if (Date != viewModel.GetDateTime() && Venue != viewModel.Venue)
            {
                changed = "both";
            }
            notifi = Notification.DitUpdated(this, Date, Venue, changed);
            Date = viewModel.GetDateTime();
            Venue = viewModel.Venue;
            GenreId = viewModel.Genre;

            foreach (var item in FaveDits!.Select(f => f.AppUser))
            {
                item.Notifi(notifi);
            }

        }
    }
}

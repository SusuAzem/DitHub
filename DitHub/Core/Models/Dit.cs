using DitHub.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace DitHub.Core.Models
{
    public class Dit
    {
        public Dit()
        {
        }
        public Dit(string id, CreateViewModel viewModel)
        {
            AppUserId = id;
            Date = viewModel.GetDateTime();
            GenreId = viewModel.Genre;
            Venue = viewModel.Venue;
        }

        public int Id { get; private set; }
        public virtual AppUser AppUser { get; private set; } = null!;
        public string AppUserId { get; private set; } = null!;
        public DateTime Date { get; set; }
        public string Venue { get; set; } = "";
        public virtual Genre Genre { get; set; } = null!;
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
                if (item.Id != AppUserId)
                {
                    item.Notifi(notifi);
                }
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
            // this param contains the old values of Date & Venue, but they are spesified here to assign them
            // in the notification oblect
            notifi = Notification.DitUpdated(this, Date, Venue, changed);
            Date = viewModel.GetDateTime();
            Venue = viewModel.Venue;
            GenreId = viewModel.Genre;

            foreach (var item in FaveDits!.Select(f => f.AppUser))
            {
                if (item.Id != AppUserId)
                {
                    item.Notifi(notifi);
                }
            }

        }
    }
}

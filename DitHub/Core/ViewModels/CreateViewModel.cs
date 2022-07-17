using DitHub.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DitHub.Core.ViewModels
{
    public class CreateViewModel
    {
        public CreateViewModel()
        {

        }
        public CreateViewModel(IEnumerable<Genre> genres)
        {
            Genres = genres;
        }
        public int Id { get; set; }

        [Required]
        public string Venue { get; set; } = "";
        public IEnumerable<Genre> Genres { get; set; } = null!;

        [Required]
        [Date(ErrorMessage = "Invalid date, date >= Now")]
        public string Date { get; set; } = "";

        [Required]
        [Time(ErrorMessage = "Invalid Time")]
        public string Time { get; set; } = "";

        [Required]
        public byte Genre { get; set; }
        public DateTime GetDateTime()
        {
            return DateTime.Parse($"{Date} {Time}");
        }
        public string Title { get; set; } = "";
        public string Action { get; set; } = "";

    }
}

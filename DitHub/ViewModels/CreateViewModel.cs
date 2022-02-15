using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DitHub.ViewModels
{
    public class CreateViewModel
    {
        [Required]
        public string Venue { get; set; } = "";
        public IEnumerable<Genre> Genres { get; set; } = null!;
       
        [Required]
        [Date(ErrorMessage = "Invalid date")]
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
    }
}

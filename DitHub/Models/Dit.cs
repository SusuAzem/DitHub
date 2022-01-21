using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace DitHub.Models
{
    public class Dit
    {
        public int Id { get; set; }
        [Required]
        public IdentityUser Artist { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Today;
        [Required]
        [StringLength(255)]
        public string Venue { get; set; } = "";
        [Required]
        public Genre Genre { get; set; }

        public Dit(IdentityUser user, Genre genre)
            => (Artist, Genre) = (user, genre);

    }
}

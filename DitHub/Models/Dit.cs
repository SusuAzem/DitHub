using System;
using System.ComponentModel.DataAnnotations;

namespace DitHub.Models
{
    public class Dit
    {
        public int Id { get; set; }
        [Required]
        public AppUser Artist { get; set; } = null!;
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(255)]
        public string Venue { get; set; } = "";
        [Required]
        public Genre Genre { get; set; } = null!;

        //public Dit(AppUser artist, Genre genre)
        //{
        //    Artist = artist;
        //    Genre = genre;
        //}
        //=> (Artist, Genre) = (user, genre);
    }
}

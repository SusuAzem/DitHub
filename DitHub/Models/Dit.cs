using System;
using System.ComponentModel.DataAnnotations;

namespace DitHub.Models
{
    public class Dit
    {
        public int Id { get; set; }
        public AppUser Artist { get; set; } = null!;
        [Required]
        public string ArtistId { get; set; } = null!;
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(255)]
        public string Venue { get; set; } = "";
        public Genre Genre { get; set; } = null!;
        [Required]
        public byte GenreId { get; set; }


        //public Dit(AppUser artist, Genre genre)
        //{
        //    Artist = artist;
        //    Genre = genre;
        //}
        //=> (Artist, Genre) = (user, genre);
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DitHub.Models
{
    public class Dit
    {
        public int Id { get; set; }
        public virtual AppUser AppUser { get; set; } = null!;
        [Required]
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; } = null!;
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(255)]
        public string Venue { get; set; } = "";
        public virtual Genre Genre { get; set; } = null!;
        [Required]
        [ForeignKey("Genre")]
        public byte GenreId { get; set; }

        public virtual ICollection<FaveDit>? FaveDits { get; set; }
        public bool RemoveFlag { get; set; }


        //public Dit(AppUser artist, Genre genre)
        //{
        //    Artist = artist;
        //    Genre = genre;
        //}
        //=> (Artist, Genre) = (user, genre);
    }
}

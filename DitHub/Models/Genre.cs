using DitHub.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DitHub
{
    public class Genre
    {
        [Required]
        public byte Id { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name = "Genre")]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Dit>? Dits { get; set; }
    }
}
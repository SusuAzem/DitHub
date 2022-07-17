using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DitHub.Core.Models
{
    public class Genre
    {
        public byte Id { get; set; }

        [Display(Name = "Genre")]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Dit>? Dits { get; set; }
    }
}
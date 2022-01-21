using System.ComponentModel.DataAnnotations;

namespace DitHub
{
    public class Genre
    {
        [Required]
        public byte Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;
    }
}
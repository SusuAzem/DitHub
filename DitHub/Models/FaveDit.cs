using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DitHub.Models
{
    public class FaveDit
    {
        public virtual Dit Dit { get; set; } = null!;

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Dit")]
        public int? DitId { get; set; }

        public virtual AppUser AppUser { get; set; } = null!;

        [Key]
        [Column(Order = 1)]
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; } = null!;
    }
}

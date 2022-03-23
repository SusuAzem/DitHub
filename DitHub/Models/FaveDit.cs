using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DitHub.Models
{
    public class FaveDit
    {
        protected FaveDit()
        {
        }
        public FaveDit(string userId, int ditid)
        {
            DitId = ditid;
            AppUserId = userId;
        }

        public virtual Dit Dit { get; private set; } = null!;

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Dit")]
        public int? DitId { get; private set; }

        public virtual AppUser AppUser { get; private set; } = null!;

        [Key]
        [Column(Order = 1)]
        [ForeignKey("AppUser")]
        public string AppUserId { get; private set; } = null!;
    }
}

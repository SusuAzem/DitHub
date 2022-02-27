using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DitHub.Models
{
    public class Following
    {
        public virtual AppUser Follower { get; set; } = null!;

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Follower")]
        public string FollowerId { get; set; } = null!;

        public virtual AppUser Followee { get; set; } = null!;

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Followee")]
        public string FolloweeId { get; set; } = null!;
    }
}

namespace DitHub.Core.Models
{
    public class Following
    {
        public virtual AppUser Follower { get; set; } = null!;
        public string FollowerId { get; set; } = null!;

        public virtual AppUser Followee { get; set; } = null!;

        public string FolloweeId { get; set; } = null!;
    }
}

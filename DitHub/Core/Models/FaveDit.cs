namespace DitHub.Core.Models
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

        public int? DitId { get; private set; }

        public virtual AppUser AppUser { get; private set; } = null!;

        public string AppUserId { get; private set; } = null!;
    }
}

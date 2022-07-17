namespace DitHub.Core.Models
{
    public class UserNotification
    {
        public virtual AppUser AppUser { get; private set; } = null!;
        public string AppUserId { get; private set; } = null!;

        public virtual Notification Notification { get; private set; } = null!;
        public int NotificationId { get; private set; }

        public bool IsRead { get; private set; }

        //this is an association class so it should always reference an existing objects and not to go in an invalid state
        //by setting properties in the custom constructor with not null values, and a default constructor for EF, 
        //and making the setters private

        protected UserNotification()
        {
        }

        public UserNotification(AppUser user, Notification notification)
        => (AppUser, Notification) = (user, notification);

        internal void Read()
        {
            IsRead = true;
        }
    }
}

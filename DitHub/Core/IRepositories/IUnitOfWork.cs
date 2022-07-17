
namespace DitHub.Core.IRepositories
{
    public interface IUnitOfWork
    {
        IDitR Dits { get; }
        IFaveDitR Favedits { get; }
        IFollowingR Followings { get; }
        IGenreR Genres { get; }
        IUserR Users { get; }
        INotificationR Notifications { get; }
        IUserNotificationR UserNotifications { get; }

        void Complete();
    }
}
using DitHub.Core;
using DitHub.Core.IRepositories;

namespace DitHub.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public IFollowingR Followings { get; private set; }
        public IFaveDitR Favedits { get; private set; }
        public IDitR Dits { get; private set; }
        public IGenreR Genres { get; private set; }
        public IUserR Users { get; private set; }


        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            Followings = new FollowingR(dbContext);
            Favedits = new FaveDitR(dbContext);
            Dits = new DitR(dbContext);
            Genres = new GenreR(dbContext);
            Users = new UserR(dbContext);
        }

        public void Complete()
        {
            dbContext.SaveChanges();
        }
    }
}

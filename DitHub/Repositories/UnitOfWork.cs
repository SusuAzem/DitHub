using DitHub.Data;

namespace DitHub.Repositories
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public FollowingR Followings { get; private set; }
        public FaveDitR Favedits { get; private set; }
        public DitR Dits { get; private set; }
        public GenreR Genres { get; private set; }
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            Followings = new FollowingR(dbContext);
            Favedits = new FaveDitR(dbContext);
            Dits = new DitR(dbContext);
            Genres = new GenreR(dbContext);
        }

        public void Complete()
        {
            dbContext.SaveChanges();
        }
    }
}

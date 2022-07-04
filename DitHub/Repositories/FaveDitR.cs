using DitHub.Data;
using System.Linq;

namespace DitHub.Repositories
{
    public class FaveDitR
    {
        private readonly ApplicationDbContext dbContext;

        public FaveDitR(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool IsInFaveD(int ditId, string userId)
        {
            return dbContext.FaveDits.Any(f => f.DitId == ditId && f.AppUserId == userId);
        }
    }
}

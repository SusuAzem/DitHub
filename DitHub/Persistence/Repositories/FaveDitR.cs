using DitHub.Core.IRepositories;
using DitHub.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace DitHub.Persistence.Repositories
{
    public class FaveDitR : IFaveDitR
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

        public IEnumerable<FaveDit> UserFaveD(string user)
        {
            return dbContext.FaveDits
                            .Where(f => f.AppUserId == user)
                            .ToList();
        }

        public void AddFavedit(FaveDit Fave)
        {
            dbContext.FaveDits.Add(Fave);
        }

        public FaveDit? GetFaveDit(int ditId, string userId)
        {
            return dbContext.FaveDits.FirstOrDefault(f => f.AppUserId == userId && f.DitId == ditId);
        }

        public void Remove(FaveDit favedit)
        {
            dbContext.FaveDits.Remove(favedit);
        }
    }
}

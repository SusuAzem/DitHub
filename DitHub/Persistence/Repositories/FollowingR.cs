using DitHub.Core.IRepositories;
using DitHub.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace DitHub.Persistence.Repositories
{
    public class FollowingR : IFollowingR
    {
        private readonly ApplicationDbContext dbContext;

        public FollowingR(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Following following)
        {
            dbContext.Followings.Add(following);
        }

        public bool IsInFaveA(string feeId, string ferId)
        {
            return dbContext.Followings
                                .Any(f => f.FolloweeId == feeId && f.FollowerId == ferId);
        }

        public Following? GetFollowing(string feeId, string ferId)
        {
            return dbContext.Followings.FirstOrDefault(f => f.FollowerId == ferId && f.FolloweeId == feeId);
        }

        public void Remove(Following following)
        {
            dbContext.Followings.Remove(following);
        }

        public IEnumerable<Following> GetUserFollowee(string id)
        {
            return dbContext.Followings
                            .Where(f => f.FollowerId == id)
                            .ToList();
        }
    }
}

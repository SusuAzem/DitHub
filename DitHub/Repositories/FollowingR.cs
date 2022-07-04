using DitHub.Data;
using DitHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace DitHub.Repositories
{
    public class FollowingR
    {
        private readonly ApplicationDbContext dbContext;

        public FollowingR(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<Following> GetUserFollowee(string id)
        {
            return dbContext.Followings
                            .Where(f => f.FollowerId == id)
                            .ToList();
        }

        public bool IsInFaveA(string feeId, string ferId)
        {
            return dbContext.Followings
                                .Any(f => f.FolloweeId == feeId && f.FollowerId == ferId);
        }
    }
}

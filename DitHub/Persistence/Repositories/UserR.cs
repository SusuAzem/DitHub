using DitHub.Core.IRepositories;
using DitHub.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace DitHub.Persistence.Repositories
{
    public class UserR : IUserR
    {
        private readonly ApplicationDbContext dbContext;

        public UserR(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<AppUser> GetUserFollowees(string id)
        {
            return dbContext.Users
                            .Where(f => f.Followers!.Where(f => f.FollowerId == id).Any())
                            .ToList();
        }
    }
}

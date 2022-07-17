using DitHub.Core.IRepositories;
using DitHub.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DitHub.Persistence.Repositories
{
    public class DitR : IDitR
    {
        private readonly ApplicationDbContext dbContext;

        public DitR(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Dit> GetDits()
        {
            return dbContext.Dits
                            .Include(d => d.AppUser)
                            .Include(d => d.Genre);

        }
        public IQueryable<Dit> GetUserFave(string id)
        {
            return dbContext.Dits
                            .Where(d => (d.FaveDits!).Any(f => f.AppUserId == id))
                            .Include(d => d.AppUser)
                            .Include(d => d.Genre);

        }

        public Dit? GetDitWithFaves(int id, string userId)
        {
            return dbContext.Dits
                .Include(d => d.FaveDits!)
                .FirstOrDefault(d => d.Id == id && d.AppUserId == userId);
        }

        public IEnumerable<Dit> GetDitWithGenra(string id)
        {
            return dbContext.Dits
                .Where(d => d.AppUserId == id)
                .Include(d => d.Genre)
                .ToList();
        }

        public Dit? GetDit(int id)
        {
            return dbContext.Dits.FirstOrDefault(d => d.Id == id);
        }

        public void Add(Dit dit)
        {
            dbContext.Add(dit);
        }
    }
}

using DitHub.Data;
using DitHub.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DitHub.Repositories
{
    public class DitR
    {
        private readonly ApplicationDbContext dbContext;

        public DitR(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Dit> GetUserFave(string? id = null)
        {
            return dbContext.Dits
                            .Where(d => (d.FaveDits!).Any(f => id == null || f.AppUserId == id))
                            .Include(d => d.AppUser)
                            .Include(d => d.Genre);

        }

        public Dit? GetDitWithFaves(int id)
        {
            return dbContext.Dits
                .Include(d => d.FaveDits!)
                .FirstOrDefault(d => d.Id == id);
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

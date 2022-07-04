using DitHub.Data;
using System.Collections.Generic;
using System.Linq;

namespace DitHub.Repositories
{
    public class GenreR
    {
        private readonly ApplicationDbContext dbContext;

        public GenreR(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Genre> GetGenres()
        {
            return dbContext.Genres.ToList();
        }
    }
}

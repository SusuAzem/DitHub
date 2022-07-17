using DitHub.Core.IRepositories;
using DitHub.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace DitHub.Persistence.Repositories
{
    public class GenreR : IGenreR
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

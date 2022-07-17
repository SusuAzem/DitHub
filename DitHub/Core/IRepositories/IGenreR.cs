using DitHub.Core.Models;
using System.Collections.Generic;

namespace DitHub.Core.IRepositories
{
    public interface IGenreR
    {
        IEnumerable<Genre> GetGenres();
    }
}
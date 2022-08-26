using DitHub.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace DitHub.Core.IRepositories
{
    public interface IDitR
    {
        void Add(Dit dit);
        Dit? GetDit(int id);
        IQueryable<Dit> GetDits();
        //Dit? GetDitWithFaves(int id, string userId);
        Dit? GetDitWithFaves(int id);

        IEnumerable<Dit> GetDitWithGenra(string id);
        IQueryable<Dit> GetUserFave(string id);
    }
}
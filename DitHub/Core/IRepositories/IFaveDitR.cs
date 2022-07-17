using DitHub.Core.Models;
using System.Collections.Generic;

namespace DitHub.Core.IRepositories
{
    public interface IFaveDitR
    {
        bool IsInFaveD(int ditId, string userId);
        IEnumerable<FaveDit> UserFaveD(string user);
        void AddFavedit(FaveDit fave);
        FaveDit? GetFaveDit(int ditid, string userId);
        void Remove(FaveDit favedit);
    }
}
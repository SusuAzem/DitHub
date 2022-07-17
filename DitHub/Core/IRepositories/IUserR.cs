using DitHub.Core.Models;
using System.Collections.Generic;

namespace DitHub.Core.IRepositories
{
    public interface IUserR
    {
        IEnumerable<AppUser> GetUserFollowees(string id);
    }
}
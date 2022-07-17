using DitHub.Core.Models;
using System.Collections.Generic;

namespace DitHub.Core.IRepositories
{
    public interface IFollowingR
    {
        bool IsInFaveA(string feeId, string ferId);
        void Add(Following following);
        Following? GetFollowing(string feeId, string userId);
        void Remove(Following following);
        IEnumerable<Following> GetUserFollowee(string id);
    }
}
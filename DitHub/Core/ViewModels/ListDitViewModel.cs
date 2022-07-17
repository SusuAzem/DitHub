using DitHub.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace DitHub.Core.ViewModels
{
    public class ListDitViewModel
    {
        public IEnumerable<Dit> Dits { get; set; } = null!;
        public string? Title { get; set; }

        public string? SearchTerm { get; set; }
        public ILookup<int?, FaveDit>? FaveDits { get; internal set; }
        public ILookup<string, Following>? FolloweeL { get; internal set; }
    }
}

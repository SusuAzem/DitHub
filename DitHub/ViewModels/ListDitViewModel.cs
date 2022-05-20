using DitHub.Models;
using System.Collections.Generic;

namespace DitHub.ViewModels
{
    public class ListDitViewModel
    {
        public IEnumerable<Dit> Dits { get; set; } = null!;
        public string? Title { get; set; }

        public string? SearchTerm { get; set; }
    }
}

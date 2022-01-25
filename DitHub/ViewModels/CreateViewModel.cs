using System.Collections.Generic;

namespace DitHub.ViewModels
{
    public class CreateViewModel
    {
        public string Venue { get; set; } = "";
        public IEnumerable<Genre> Genres { get; set; } = null!;
        public string Date { get; set; } = "";
        public string Time { get; set; } = "";
    }
}

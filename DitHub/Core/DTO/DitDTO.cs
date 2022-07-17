using System;

namespace DitHub.Core.DTO
{
    public class DitDTO
    {
        public int Id { get; set; }
        public AppUserDTO AppUser { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Venue { get; set; } = "";
        public GenreDTO Genre { get; set; } = null!;
        public bool RemoveFlag { get; set; }

    }
}
using DitHub.Core.Models;

namespace DitHub.Core.ViewModels
{
    public class DetailsViewModel
    {
        public Dit Dit { get; set; } = null!;
        public bool Following { get; set; }
        public bool Infave { get; set; }
    }
}
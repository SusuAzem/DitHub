using AutoMapper;
using DitHub.DTO;

namespace DitHub.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, AppUserDTO>();
            CreateMap<Genre, GenreDTO>();
            CreateMap<Dit, DitDTO>();
            CreateMap<Notification, NotificationDTO>();
        }
    }

}

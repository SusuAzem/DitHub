using AutoMapper;
using DitHub.Core.DTO;
using DitHub.Core.Models;

namespace DitHub.Core
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

using AutoMapper;
using DitHub.Data;
using DitHub.DTO;
using DitHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DitHub.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;
        public NotificationsController(ApplicationDbContext dbContext, UserManager<AppUser> userManager, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        [HttpGet]
        public IEnumerable<NotificationDTO> GetNotifications()
        {
            var notifications = dbContext.Notifications
                .Include(n => n.Dit.AppUser)
                .Where(n => n.UserNotifications!
                .Where(un => un.AppUserId == userManager.GetUserId(User) && !un.IsRead).Any())
                .ToList();

            List<NotificationDTO> dto = mapper.Map<List<Notification>, List<NotificationDTO>>(notifications);

            return dto;
            //    notifications.Select(
            //n => new NotificationDTO()
            //{
            //    DateTime = n.DateTime,
            //    Type = n.Type,
            //    DateTime0 = n.DateTime0,
            //    Venue0 = n.Venue0,
            //    Dit = new DitDTO()
            //    {
            //        Id = n.Dit.Id,
            //        Date = n.Dit.Date,
            //        Venue = n.Dit.Venue,
            //        RemoveFlag = n.Dit.RemoveFlag,
            //        AppUser = new AppUserDTO()
            //        {
            //            Id = n.Dit.AppUser.Id,
            //            Name = n.Dit.AppUser.Name,
            //        },
            //    }
            //});
        }
    }
}

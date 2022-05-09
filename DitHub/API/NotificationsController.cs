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
        public List<NotificationDTO> GetNotifications()
        {
            var notifications = dbContext.Notifications
                .Include(n => n.Dit.AppUser)
                .Where(n => n.UserNotifications!
                .Where(un => un.AppUserId == userManager.GetUserId(User) && !un.IsRead).Any())
                .ToList();

            List<NotificationDTO> dto1 = mapper.Map<List<Notification>, List<NotificationDTO>>(notifications);
            foreach (var item in dto1)
            {
                item.Statue = "new";
            }

            var oldnotification = dbContext.Notifications
            .Include(n => n.Dit.AppUser)
            .Where(n => n.UserNotifications!
            .Where(un => un.AppUserId == userManager.GetUserId(User)).Any())
            .OrderByDescending(n => n.DateTime)
            .Take(3)
            .ToList();

            List<NotificationDTO> dto2 = mapper.Map<List<Notification>, List<NotificationDTO>>(oldnotification);
            foreach (var item in dto2)
            {
                item.Statue = "old";
            }

            dto2.ForEach(item => dto1.Add(item));
            return dto1;
        }

        [HttpPost("makeread")]
        [IgnoreAntiforgeryToken]
        public IActionResult MakeRead()
        {
            var notifications = dbContext.UserNotifications
               .Where(un => un.AppUserId == userManager.GetUserId(User) && !un.IsRead)
               .ToList();
            notifications.ForEach(un => un.Read());
            dbContext.SaveChanges();
            return Ok();
        }
    }
}

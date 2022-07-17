using AutoMapper;
using DitHub.Core;
using DitHub.Core.DTO;
using DitHub.Core.IRepositories;
using DitHub.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DitHub.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        public NotificationsController(UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork unit)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.unit = unit;
        }
        [HttpGet]
        public List<NotificationDTO> GetNotifications()
        {
            var id = userManager.GetUserId(User);
            var notifications = unit.Notifications.GetNewNotifi(id);

            List<NotificationDTO> dto1 = mapper.Map<List<Notification>, List<NotificationDTO>>(notifications.ToList());
            foreach (var item in dto1)
            {
                item.Statue = "new";
            }
            var oldnotification = unit.Notifications.Get3OldNotifi(id);

            List<NotificationDTO> dto2 = mapper.Map<List<Notification>, List<NotificationDTO>>(oldnotification.ToList());
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
            var id = userManager.GetUserId(User);
            var notifications = unit.UserNotifications.GetUserNewNotifi(id).ToList();
            notifications.ForEach(un => un.Read());
            unit.Complete();
            return StatusCode(204);
        }


    }
}

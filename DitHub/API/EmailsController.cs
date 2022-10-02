using AutoMapper.Internal;
using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using EmailService;
using System.Collections.Generic;
using System.Linq;

namespace DitHub.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailSender mailService;
        public EmailsController(IEmailSender mailService)
        {
            this.mailService = mailService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var message = new Message("" ,"sososawas86@gmail.com", "Test email async", "This is the content from our email.", null);
            await mailService.SendEmailAsync(message);
            return Ok();
        }


        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Post()
        {          
            var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();

            var message = new Message("sososawas86@gmail.com", "s-thepaintedlady@hotmail.com", "Test email async", "This is the content from our async email.", files);
            await mailService.SendEmailAsync(message);

            return Ok();
        }
    }
}

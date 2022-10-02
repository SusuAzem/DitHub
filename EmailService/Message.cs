using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    public class Message
    {
        public MailboxAddress To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public IFormFileCollection Attachments { get; set; }
        public Message(string username, string to, string subject, string content, IFormFileCollection attachments)
        {
            To = new MailboxAddress(username,to);
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
}

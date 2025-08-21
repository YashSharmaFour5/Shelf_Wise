using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly string messageFrom;
        private readonly string userName;
        private readonly string password;
        public EmailSender(IConfiguration config)
        {
            var section = config.GetRequiredSection("EmailSender");
            messageFrom = section["FromEmail"];
            userName = section["UserName"];
            password = section["Password"];
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailToSend = new MimeMessage()
            {
                From = { new MailboxAddress("Shelf Wise BookStore", messageFrom)},
                To = { MailboxAddress.Parse(email)},
                Subject = subject,
                Body = new TextPart(TextFormat.Html) { Text = htmlMessage }
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.sendgrid.net", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(userName, password);
                client.Send(emailToSend);
                client.Disconnect(true);
            }
            
            return Task.CompletedTask;
        }
    }
}

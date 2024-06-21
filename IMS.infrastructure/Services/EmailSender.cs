using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IMS.infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string fromMail = _config["EmailSender:fromEmail"];
            string fromPassword = _config["EmailSender:fromPassword"];


            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(email));
            message.Body = "<html><body>" + htmlMessage + "</body></html>";
            message.IsBodyHtml = true;


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(fromMail, fromPassword)
            };

            smtp.Send(message);
            return Task.CompletedTask;

        }
        

     



    }
}

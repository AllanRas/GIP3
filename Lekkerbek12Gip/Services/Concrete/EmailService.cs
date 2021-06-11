using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Models.Mails;
using Lekkerbek12Gip.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Concrete
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Send(IEmail mail1, Klant user = null)
        {
            var from = _configuration.GetSection("Email").GetSection("From").Value;
            var password = _configuration.GetSection("Email").GetSection("Password").Value;
            MailMessage mail = new MailMessage();
            mail.To.Add(user.emailadres);
            mail.From = new MailAddress(from);
            mail.Subject = "Order";
            mail.Body = mail1.Message;

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient(port: 587, host: "smtp.gmail.com");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(from, password);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}

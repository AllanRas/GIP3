using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Models.Mails;
using Lekkerbek12Gip.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IBesteldeGerectenService _bestellingGerechtenService;
        private readonly LekkerbekContext _context;
        public EmailService(IConfiguration configuration, IBesteldeGerectenService bestellingGerechtenService, LekkerbekContext context)
        {
            _configuration = configuration;
            _bestellingGerechtenService = bestellingGerechtenService;
            _context = context;
        }
        public async Task Send(IEmail mail1,int id)
        {
           
                var klant = _context.Bestellings.Include(x => x.Klant).FirstOrDefault(x => x.BestellingId == id).Klant;
                var bestellings=await _bestellingGerechtenService.GetBestellingGerechtenwithIncludeFilter(x => x.BestellingId == id);
                mail1.Bestellings = bestellings.ToList();
           
            var from = _configuration.GetSection("Email").GetSection("From").Value;
            var password = _configuration.GetSection("Email").GetSection("Password").Value;
            if (klant.emailadres != null) { 
            MailMessage mail = new MailMessage();
            mail.To.Add(klant.emailadres);
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
}

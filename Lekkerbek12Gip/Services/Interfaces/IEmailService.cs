using Lekkerbek12Gip.Models.Mails;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Interfaces
{
   public interface IEmailService
    {
        void Send(IEmail mail,IdentityUser user = null);
    }
}

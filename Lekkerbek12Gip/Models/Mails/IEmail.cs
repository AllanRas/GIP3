using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models.Mails
{
   public interface IEmail
    {
        public string Message { get;}
        public List<BestellingGerechten> Bestellings { get; set; }
    }
}

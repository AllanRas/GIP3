using Lekkerbek12Gip.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class BestellingDrank
    {
        public int? BestellingId { get; set; }
        public int? DrankId { get; set; }
        public virtual Bestelling Bestelling { get; set; }
        public virtual Drank Drank{ get; set; }
        public int Aantal { get; set; }
    }
}

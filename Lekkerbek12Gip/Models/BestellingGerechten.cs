using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class BestellingGerechten
    {       
        public int? BestellingId { get; set; }
        public int? GerechtId { get; set; }
        public virtual Bestelling Bestelling { get; set; }
        public virtual Gerecht Gerecht { get; set; }
        public int Aantal { get; set; }
    }
}

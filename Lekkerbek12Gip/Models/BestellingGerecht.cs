using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class BestellingGerecht
    {
        public int BestellingId { get; set; }
        public int GerechtId { get; set; }
        public Bestelling Bestelling { get; set; }
        public Gerecht Gerecht { get; set; }
        public int Aantal { get; set; }
    }
}

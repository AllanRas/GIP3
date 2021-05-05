using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class GerechtKlantFavoriet
    {
        public int? KlantId { get; set; }
        public int? GerechtId { get; set; }
        public virtual Klant Klant { get; set; }
        public virtual Gerecht Gerecht { get; set; }
    }
}

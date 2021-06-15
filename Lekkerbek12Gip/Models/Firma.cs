using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class Firma
    {
        public int FirmaId { get; set; }
        public String FirmaNaam { get; set; }
        [RegularExpression(@"[0-9]{12}", ErrorMessage = "btw nummer moet nummer 12 zijn")]
        public String BtwNummer { get; set; }
        //public int? KlantId { get; set; }
        //public virtual Klant Klant { get; set; }
    }
}

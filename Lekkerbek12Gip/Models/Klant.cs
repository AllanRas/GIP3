using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class Klant
    {
        public int KlantId { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public int GetrouwheidsScore{ get; set; }
        public DateTime Geboortedatum { get; set; }
        public virtual ICollection<Bestelling> Bestellings { get; set; }
    }
}

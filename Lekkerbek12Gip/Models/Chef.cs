using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class Chef
    {
        public int ChefId { get; set; }
        public string ChefName { get; set; }
        public virtual ICollection<Bestelling> Bestellings { get; set; }

    }
}

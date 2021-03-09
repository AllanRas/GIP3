using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class Gerecht
    {
        public Gerecht()
        {
            this.Bestellingen = new HashSet<Bestelling>();
        }

        public enum Categorieën
        {
            Sandwiches,
            Salades,
            Vleesgerechten,
            Visgerechten,
        }
        public int GerechtId { get; set; }
        public string Naam { get; set; }
        public string Omschrijving { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Prijs { get; set; }
        public Categorieën Categorie { get; set; }
        public int? Aantal { get; set; }
        public virtual ICollection<Bestelling> Bestellingen { get; set; }
    }
}

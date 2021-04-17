using Lekkerbek12Gip.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class Klant
    {
        public int KlantId { get; set; }
        [Required(ErrorMessage = "Naam is verplicht")]
        [Display(Name = "Naam")]
        public string Name { get; set; }
        public string Adress { get; set; }
        public int GetrouwheidsScore { get; set; }
        [AdultBirthdateAttribute]
        [DataType(DataType.Date)]
        public DateTime Geboortedatum { get; set; }
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Adress")]
        public string emailadres { get; set; }

        public virtual ICollection<Bestelling> Bestellings { get; set; }
    }
}

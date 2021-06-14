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
        public Klant()
        {
            if (Fav==null)
            {
                Fav = new List<Gerecht>();
            }
           
        }
        [Required(ErrorMessage = "Naam is verplicht")]
        [Display(Name = "Naam")]
        public string Name { get; set; }
        [Display(Name = "Adres")]
        public string Adress { get; set; }
        [Display(Name = "KlantenScore")]
        public int GetrouwheidsScore { get; set; }
        [AdultBirthdateAttribute]
        [DataType(DataType.Date)]
        public DateTime Geboortedatum { get; set; }
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Emailadres")]
        public string emailadres { get; set; }
        public int FirmaId { get; set; }
        public virtual Firma Firma { get; set; }
        public virtual ICollection<Bestelling> Bestellings { get; set; }
        public virtual ICollection<Gerecht> Fav { get; set; }


    }
}

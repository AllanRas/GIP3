using Lekkerbek12Gip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.ViewModel
{
    public class BesteldeGerechtenIndexModel
    {
      
        public List<BestellingGerechten> BestellingGerechten { get; set; }
        public List<Gerecht> Gerechts{ get; set; }
        public List<Gerecht> favGerechten { get; set; }
        public List<BestellingGerechten> ToegevoegdGerechtens { get; set; }

        public int Aantalmatcher(int gerechtId,int bestellingId)
        {
            var aantal= BestellingGerechten.Where(x => (x.GerechtId == gerechtId) && (x.BestellingId == bestellingId)).FirstOrDefault();
           return aantal==null?0:aantal.Aantal<0?0:aantal.Aantal;
        }
    }
}

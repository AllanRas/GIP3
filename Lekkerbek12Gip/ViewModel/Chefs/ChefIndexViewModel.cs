using Lekkerbek12Gip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.ViewModel.Chefs
{
    public class ChefIndexViewModel
    {
        public List<Bestelling> BestellingsNotReadyList { get; set; }
        public List<Bestelling> BestellingsReadyList { get; set; }
        public List<Bestelling> BestellingsDoneList { get; set; }

        public List<Bestelling> ReturnStatusList(Bestelling.BestelStatus status)
        {
            if (status == Bestelling.BestelStatus.Done) return BestellingsDoneList;
            if (status == Bestelling.BestelStatus.NotReady) return BestellingsNotReadyList;

            else return BestellingsReadyList;
        }
    }
}

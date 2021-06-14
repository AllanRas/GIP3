using Lekkerbek12Gip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.ViewModel.Gerechten
{
    public class GerechtenIndexModel
    {
        public List<Gerecht> Gerechts{ get; set; }
        public List<Category> Categories{ get; set; }
    }
}

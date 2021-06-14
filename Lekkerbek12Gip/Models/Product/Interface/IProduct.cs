using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models.Product.Interface
{

    public interface IProduct
    {
        public decimal Prijs { get; set; }
        public decimal WithBtwPrijs { get; }
        int GetBelasting();

    }
}

using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Concrete
{
    public class KlantService:RepositoryBase<Klant>,IKlantsService
    {
        private readonly LekkerbekContext _context;
        public KlantService(LekkerbekContext context):base(context)
        {
            _context = context;
        }
    }
}

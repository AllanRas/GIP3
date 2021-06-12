using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Concrete
{
    public class ChefService:RepositoryBase<Chef>,IChefsService
    {
        private readonly LekkerbekContext _context;

        public ChefService(LekkerbekContext context):base(context)
        {
            _context = context;
        }
    }
}

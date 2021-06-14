using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Concrete
{
    public class CategoryService : RepositoryBase<Category>, ICategoryService
    {
        private readonly LekkerbekContext _context;

        public CategoryService(LekkerbekContext context) : base(context)
        {
            _context = context;
        }
    }
}

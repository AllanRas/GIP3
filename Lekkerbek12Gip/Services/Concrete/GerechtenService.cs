using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Interfaces;
using Lekkerbek12Gip.ViewModel.Gerechten;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Concrete
{
    public class GerechtenService : RepositoryBase<Gerecht>, IGerechtenService
    {
        private readonly LekkerbekContext _context;
        private readonly ICategoryService _categoryService;
        public GerechtenService(LekkerbekContext context, ICategoryService categoryService) : base(context)
        {
            _context = context;
            _categoryService = categoryService;
        }

        public async Task<GerechtenIndexModel> GerechtenIndexModel()
        {
            GerechtenIndexModel indexModel = new GerechtenIndexModel();
            indexModel.Gerechts = await _context.Gerechten
                .Include(x => x.Category)
                .ToListAsync();
            indexModel.Categories = (List<Category>)await _categoryService.GetList();
            indexModel.Dranken = await _context.Dranken
                .Include(x => x.Category)
                .ToListAsync();
            return indexModel;

        }

        public async Task<Gerecht> GetGerechtWithIncludeFilter(Expression<Func<Gerecht, bool>> filter)
        {
            var drank = await _context.Gerechten
                .Include(d => d.Category)
                .FirstOrDefaultAsync(filter);
            return drank;
        }
    }
}

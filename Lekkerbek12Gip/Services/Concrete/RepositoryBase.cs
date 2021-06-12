using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Concrete
{
    public class RepositoryBase<Entity> :IEntityRepositoryBase<Entity>
        where Entity:class,new()        
    {
        private  LekkerbekContext _context;
        public RepositoryBase(LekkerbekContext context)
        {           
            _context = context;
        }

        public async Task<Entity> Add(Entity entity)
        {
            _context.Add(entity);
           await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Entity entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            
        }

        public async Task<Entity> Get(Expression<Func<Entity, bool>> filter = null)
        {
          var entity =_context.Set<Entity>().FirstOrDefault(filter);
           await _context.SaveChangesAsync();            
            return entity;
        }

        public async Task<IEnumerable<Entity>> GetList(Expression<Func<Entity, bool>> filter = null)
        {
            return filter == null ?
                 await _context.Set<Entity>().ToListAsync()
                 :await _context.Set<Entity>().Where(filter).ToListAsync();
        }

        public async Task<Entity> Update(Entity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

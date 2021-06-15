using Lekkerbek12Gip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Interfaces
{
    public interface IKlantsService : IEntityRepositoryBase<Klant>
    {
        // Viewbag get _context.Klants
        IEnumerable<Klant> GetKlantenIE();
        Task AddFav(int gerechtId, int bestellingId);
        Task DelFav(int gerechtId, int bestellingId);

    }
}

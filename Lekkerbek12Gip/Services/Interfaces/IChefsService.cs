using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.ViewModel.Chefs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Interfaces
{
    public interface IChefsService : IEntityRepositoryBase<Chef>
    {
        int GetNumberOfBestellingForAChef(int? id, int bestellingId);
        Task<ChefIndexViewModel> GetChefIndexViewModel();
    }
}

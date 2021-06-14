using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.ViewModel.Gerechten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Interfaces
{
    public interface IGerechtenService : IEntityRepositoryBase<Gerecht>
    {
        Task<GerechtenIndexModel> GerechtenIndexModel();
    }
}

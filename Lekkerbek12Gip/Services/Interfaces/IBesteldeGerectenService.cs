using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Interfaces
{
    public interface IBesteldeGerectenService : IEntityRepositoryBase<BestellingGerechten>
    {
        Task<IEnumerable<BestellingGerechten>> GetAllBestellingGerechtenwithInclude(ClaimsPrincipal user = null);
        Task<IEnumerable<BestellingGerechten>> GetBestellingGerechtenwithIncludeFilter(Expression<Func<BestellingGerechten, bool>> filter);
        Task<BesteldeGerechtenIndexModel> besteldeGerechtenIndexModel(int Bestelid);
        Task Gerechten(int bestellingId, int gerechtId, int aantal);
        Task<string> ConfirmBestelling(int bestellingId, string specialeWensen);
    }
}

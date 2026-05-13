using AquarelaApi.Models;

namespace AquarelaApi.Repositories.Interfaces;

public interface IDividaRepository : IRepository<Divida>
{
    Task<IEnumerable<Divida>> GetByUsuarioIdAsync(int idUsuario);
}

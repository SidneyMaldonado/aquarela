using AquarelaApi.Models;

namespace AquarelaApi.Repositories.Interfaces;

public interface IContaRepository : IRepository<Conta>
{
    Task<IEnumerable<Conta>> GetByUsuarioIdAsync(int idUsuario);
}

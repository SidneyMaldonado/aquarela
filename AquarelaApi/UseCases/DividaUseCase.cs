using AquarelaApi.Models;
using AquarelaApi.Repositories.Interfaces;

namespace AquarelaApi.UseCases;

public class DividaUseCase
{
    private readonly IDividaRepository _repository;

    public DividaUseCase(IDividaRepository repository) => _repository = repository;

    public Task<IEnumerable<Divida>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Divida?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task<IEnumerable<Divida>> GetByUsuarioIdAsync(int idUsuario) => _repository.GetByUsuarioIdAsync(idUsuario);
    public Task<Divida> CreateAsync(Divida divida) => _repository.CreateAsync(divida);
    public Task<Divida> UpdateAsync(Divida divida) => _repository.UpdateAsync(divida);
    public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
}

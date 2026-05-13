using AquarelaApi.Models;
using AquarelaApi.Repositories.Interfaces;

namespace AquarelaApi.UseCases;

public class UsuarioUseCase
{
    private readonly IUsuarioRepository _repository;

    public UsuarioUseCase(IUsuarioRepository repository) => _repository = repository;

    public Task<IEnumerable<Usuario>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Usuario?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task<Usuario> CreateAsync(Usuario usuario) => _repository.CreateAsync(usuario);
    public Task<Usuario> UpdateAsync(Usuario usuario) => _repository.UpdateAsync(usuario);
    public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
}

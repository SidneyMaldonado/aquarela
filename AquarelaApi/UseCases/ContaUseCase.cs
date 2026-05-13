using AquarelaApi.Models;
using AquarelaApi.Repositories.Interfaces;

namespace AquarelaApi.UseCases;

public class ContaUseCase
{
    private readonly IContaRepository _repository;

    public ContaUseCase(IContaRepository repository) => _repository = repository;

    public Task<IEnumerable<Conta>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Conta?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task<IEnumerable<Conta>> GetByUsuarioIdAsync(int idUsuario) => _repository.GetByUsuarioIdAsync(idUsuario);
    public Task<Conta> CreateAsync(Conta conta) => _repository.CreateAsync(conta);
    public Task<Conta> UpdateAsync(Conta conta) => _repository.UpdateAsync(conta);
    public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
}

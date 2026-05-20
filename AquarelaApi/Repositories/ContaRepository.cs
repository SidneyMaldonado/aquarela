using AquarelaApi.Contexts;
using AquarelaApi.Models;
using AquarelaApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AquarelaApi.Repositories;

public class ContaRepository : IContaRepository
{
    private readonly AppDbContext _context;

    public ContaRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Conta>> GetAllAsync()
        => await _context.Contas.Include(c => c.Usuario).ToListAsync();

    public async Task<Conta?> GetByIdAsync(int id)
        => await _context.Contas.Include(c => c.Usuario).FirstOrDefaultAsync(c => c.IdConta == id);

    public async Task<IEnumerable<Conta>> GetByUsuarioIdAsync(int idUsuario)
        => await _context.Contas.Where(c => c.IdUsuario == idUsuario).ToListAsync();

    public async Task<Conta> CreateAsync(Conta entity)
    {
        _context.Contas.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Conta> UpdateAsync(Conta entity)
    {
        // Desanexar qualquer instância já rastreada
        var tracked = _context.ChangeTracker.Entries<Conta>()
            .FirstOrDefault(e => e.Entity.IdConta == entity.IdConta);
        if (tracked != null)
        {
            tracked.State = EntityState.Detached;
        }

        _context.Contas.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is not null)
        {
            _context.Contas.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

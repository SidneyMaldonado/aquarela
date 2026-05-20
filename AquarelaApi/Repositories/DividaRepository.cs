using AquarelaApi.Contexts;
using AquarelaApi.Models;
using AquarelaApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AquarelaApi.Repositories;

public class DividaRepository : IDividaRepository
{
    private readonly AppDbContext _context;

    public DividaRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Divida>> GetAllAsync()
        => await _context.Dividas.Include(d => d.Usuario).ToListAsync();

    public async Task<Divida?> GetByIdAsync(int id)
        => await _context.Dividas.Include(d => d.Usuario).FirstOrDefaultAsync(d => d.IdDivida == id);

    public async Task<IEnumerable<Divida>> GetByUsuarioIdAsync(int idUsuario)
        => await _context.Dividas.Where(d => d.IdUsuario == idUsuario).ToListAsync();

    public async Task<Divida> CreateAsync(Divida entity)
    {
        _context.Dividas.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Divida> UpdateAsync(Divida entity)
    {
        _context.Dividas.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is not null)
        {
            _context.Dividas.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

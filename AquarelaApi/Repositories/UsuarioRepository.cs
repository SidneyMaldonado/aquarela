using AquarelaApi.Contexts;
using AquarelaApi.Models;
using AquarelaApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AquarelaApi.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Usuario>> GetAllAsync()
        => await _context.Usuarios.ToListAsync();

    public async Task<Usuario?> GetByIdAsync(int id)
        => await _context.Usuarios.FindAsync(id);

    public async Task<Usuario?> GetByEmailAsync(string email)
        => await _context.Usuarios.FirstOrDefaultAsync(u => u.DsEmail == email);

    public async Task<Usuario> CreateAsync(Usuario entity)
    {
        _context.Usuarios.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Usuario> UpdateAsync(Usuario entity)
    {
        _context.Usuarios.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is not null)
        {
            _context.Usuarios.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

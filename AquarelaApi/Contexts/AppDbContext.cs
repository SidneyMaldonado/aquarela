using AquarelaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AquarelaApi.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Conta> Contas { get; set; }
    public DbSet<Divida> Dividas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Conta>()
            .Property(c => c.NrSaldo)
            .HasColumnType("numeric(10,2)");

        modelBuilder.Entity<Divida>()
            .Property(d => d.NrValor)
            .HasColumnType("numeric(10,2)");
    }
}

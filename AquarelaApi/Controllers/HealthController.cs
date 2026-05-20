using AquarelaApi.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AquarelaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly AppDbContext _context;

    public HealthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "healthy",
            timestamp = DateTime.UtcNow,
            service = "AquarelaApi"
        });
    }

    [HttpGet("database")]
    public async Task<IActionResult> CheckDatabase()
    {
        try
        {
            // Tenta conectar e executar uma query simples
            var canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                return StatusCode(503, new
                {
                    status = "unhealthy",
                    message = "Não foi possível conectar ao banco de dados",
                    timestamp = DateTime.UtcNow
                });
            }

            // Tenta executar uma query para contar usuários (valida se as tabelas existem)
            var userCount = await _context.Usuarios.CountAsync();

            return Ok(new
            {
                status = "healthy",
                message = "Conexão com o banco de dados OK",
                database = _context.Database.GetConnectionString()?.Split(';').FirstOrDefault(s => s.Contains("Initial Catalog"))?.Split('=').LastOrDefault() ?? "N/A",
                userCount,
                timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            return StatusCode(503, new
            {
                status = "unhealthy",
                message = "Erro ao conectar com o banco de dados",
                error = ex.Message,
                innerError = ex.InnerException?.Message,
                timestamp = DateTime.UtcNow
            });
        }
    }

    [HttpGet("database/detailed")]
    public async Task<IActionResult> CheckDatabaseDetailed()
    {
        var checks = new Dictionary<string, object>();

        try
        {
            // 1. Testa conexão básica
            var canConnect = await _context.Database.CanConnectAsync();
            checks["connection"] = new { status = canConnect ? "ok" : "failed", canConnect };

            if (!canConnect)
            {
                return StatusCode(503, new
                {
                    status = "unhealthy",
                    checks,
                    timestamp = DateTime.UtcNow
                });
            }

            // 2. Verifica tabela Usuarios
            try
            {
                var userCount = await _context.Usuarios.CountAsync();
                checks["usuarios_table"] = new { status = "ok", count = userCount };
            }
            catch (Exception ex)
            {
                checks["usuarios_table"] = new { status = "error", message = ex.Message };
            }

            // 3. Verifica tabela Contas
            try
            {
                var contaCount = await _context.Contas.CountAsync();
                checks["contas_table"] = new { status = "ok", count = contaCount };
            }
            catch (Exception ex)
            {
                checks["contas_table"] = new { status = "error", message = ex.Message };
            }

            // 4. Verifica tabela Dividas
            try
            {
                var dividaCount = await _context.Dividas.CountAsync();
                checks["dividas_table"] = new { status = "ok", count = dividaCount };
            }
            catch (Exception ex)
            {
                checks["dividas_table"] = new { status = "error", message = ex.Message };
            }

            // 5. Informações da conexão
            var connectionString = _context.Database.GetConnectionString();
            checks["connection_info"] = new
            {
                database = connectionString?.Split(';').FirstOrDefault(s => s.Contains("Initial Catalog"))?.Split('=').LastOrDefault() ?? "N/A",
                server = connectionString?.Split(';').FirstOrDefault(s => s.Contains("Server"))?.Split('=').LastOrDefault() ?? "N/A"
            };

            var allHealthy = checks.Values.All(c =>
            {
                var status = c.GetType().GetProperty("status")?.GetValue(c)?.ToString();
                return status == "ok";
            });

            return allHealthy
                ? Ok(new { status = "healthy", checks, timestamp = DateTime.UtcNow })
                : StatusCode(503, new { status = "degraded", checks, timestamp = DateTime.UtcNow });
        }
        catch (Exception ex)
        {
            checks["global_error"] = new { status = "error", message = ex.Message };
            return StatusCode(503, new
            {
                status = "unhealthy",
                checks,
                timestamp = DateTime.UtcNow
            });
        }
    }
}

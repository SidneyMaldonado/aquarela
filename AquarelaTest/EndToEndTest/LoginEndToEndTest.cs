using AquarelaApi.Contexts;
using AquarelaApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace AquarelaTest.EndToEndTest;

public class AquarelaWebFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove todos os descritores relacionados ao AppDbContext,
            // incluindo IDbContextOptionsConfiguration<AppDbContext> que carrega o SQL Server
            var toRemove = services
                .Where(d =>
                    d.ServiceType == typeof(AppDbContext) ||
                    d.ServiceType == typeof(DbContextOptions<AppDbContext>) ||
                    d.ServiceType == typeof(DbContextOptions) ||
                    (d.ServiceType.IsGenericType &&
                     d.ServiceType.GenericTypeArguments.Length == 1 &&
                     d.ServiceType.GenericTypeArguments[0] == typeof(AppDbContext)))
                .ToList();

            foreach (var d in toRemove)
                services.Remove(d);

            // Registra o AppDbContext com InMemory
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("EndToEndTestDb"));
        });

        return base.CreateHost(builder);
    }
}

[TestClass]
public class LoginEndToEndTest
{
    private static AquarelaWebFactory _factory = null!;
    private static HttpClient _client = null!;

    [ClassInitialize]
    public static void ClassInitialize(TestContext _)
    {
        _factory = new AquarelaWebFactory();
        _client = _factory.CreateClient();

        // Seed: insere o usuário de teste no banco InMemory
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!context.Usuarios.Any())
        {
            context.Usuarios.Add(new Usuario
            {
                IdUsuario = 1,
                NmUsuario = "Admin",
                DsEmail = "admin@mail.com",
                DsSenha = "senha",
                DmAtivo = true
            });
            context.SaveChanges();
        }
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [TestMethod]
    public async Task Login_ComCredenciaisValidas_DeveRetornar200ComToken()
    {
        // Arrange
        var payload = new { Email = "admin@mail.com", Senha = "senha" };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", payload);
        var body = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(body).RootElement;

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
            $"Esperado 200 OK, recebido {(int)response.StatusCode}. Body: {body}");

        Assert.IsTrue(json.TryGetProperty("token", out var tokenProp),
            $"Resposta não contém a propriedade 'token'. Body: {body}");

        Assert.IsFalse(string.IsNullOrWhiteSpace(tokenProp.GetString()),
            "O token retornado não deve ser vazio.");
    }

    [TestMethod]
    public async Task Login_ComSenhaErrada_DeveRetornar401()
    {
        // Arrange
        var payload = new { Email = "admin@mail.com", Senha = "senha_errada" };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", payload);

        // Assert
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode,
            $"Esperado 401 Unauthorized, recebido {(int)response.StatusCode}.");
    }

    [TestMethod]
    public async Task Login_ComEmailErrado_DeveRetornar401()
    {
        // Arrange
        var payload = new { Email = "naoexiste@mail.com", Senha = "senha" };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", payload);

        // Assert
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode,
            $"Esperado 401 Unauthorized, recebido {(int)response.StatusCode}.");
    }
}

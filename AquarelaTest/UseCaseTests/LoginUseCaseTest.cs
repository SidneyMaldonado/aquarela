using AquarelaApi.Contexts;
using AquarelaApi.Models;
using AquarelaApi.Repositories;
using AquarelaApi.UseCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AquarelaTest.UseCaseTests;

[TestClass]
public class LoginUseCaseTest
{
    private AppDbContext CriarContextoInMemory()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    private IConfiguration CriarConfiguracao()
    {
        var configData = new Dictionary<string, string?>
        {
            { "Jwt:Key", "ChaveSecretaSuperSeguraParaTestesDaAquarelaApiComMaisDe256Bits12345" },
            { "Jwt:Issuer", "AquarelaApiTest" },
            { "Jwt:Audience", "AquarelaApiTestAudience" }
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(configData)
            .Build();
    }

    [TestMethod]
    public async Task ExecuteAsync_ComCredenciaisValidas_DeveRetornarToken()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = new Usuario
        {
            NmUsuario = "Usuario Teste",
            DsEmail = "teste@aquarela.com",
            DsSenha = "senha123",
            DmAtivo = true
        };
        context.Usuarios.Add(usuario);
        context.SaveChanges();

        var repository = new UsuarioRepository(context);
        var configuration = CriarConfiguracao();
        var useCase = new LoginUseCase(repository, configuration);

        // Act
        var token = await useCase.ExecuteAsync("teste@aquarela.com", "senha123");

        // Assert
        Assert.IsNotNull(token);
        Assert.IsTrue(token.Length > 0);
        Assert.IsTrue(token.Contains('.'), "Token JWT deve conter pontos separadores");
    }

    [TestMethod]
    public async Task ExecuteAsync_ComEmailInvalido_DeveRetornarNull()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = new Usuario
        {
            NmUsuario = "Usuario Teste",
            DsEmail = "teste@aquarela.com",
            DsSenha = "senha123",
            DmAtivo = true
        };
        context.Usuarios.Add(usuario);
        context.SaveChanges();

        var repository = new UsuarioRepository(context);
        var configuration = CriarConfiguracao();
        var useCase = new LoginUseCase(repository, configuration);

        // Act
        var token = await useCase.ExecuteAsync("emailinvalido@aquarela.com", "senha123");

        // Assert
        Assert.IsNull(token);
    }

    [TestMethod]
    public async Task ExecuteAsync_ComSenhaErrada_DeveRetornarNull()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = new Usuario
        {
            NmUsuario = "Usuario Teste",
            DsEmail = "teste@aquarela.com",
            DsSenha = "senha123",
            DmAtivo = true
        };
        context.Usuarios.Add(usuario);
        context.SaveChanges();

        var repository = new UsuarioRepository(context);
        var configuration = CriarConfiguracao();
        var useCase = new LoginUseCase(repository, configuration);

        // Act
        var token = await useCase.ExecuteAsync("teste@aquarela.com", "senhaerrada");

        // Assert
        Assert.IsNull(token);
    }

    [TestMethod]
    public async Task ExecuteAsync_ComUsuarioInativo_DeveRetornarNull()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = new Usuario
        {
            NmUsuario = "Usuario Inativo",
            DsEmail = "inativo@aquarela.com",
            DsSenha = "senha123",
            DmAtivo = false // Usuário inativo
        };
        context.Usuarios.Add(usuario);
        context.SaveChanges();

        var repository = new UsuarioRepository(context);
        var configuration = CriarConfiguracao();
        var useCase = new LoginUseCase(repository, configuration);

        // Act
        var token = await useCase.ExecuteAsync("inativo@aquarela.com", "senha123");

        // Assert
        Assert.IsNull(token);
    }
}

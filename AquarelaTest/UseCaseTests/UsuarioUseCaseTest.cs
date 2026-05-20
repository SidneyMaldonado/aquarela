using AquarelaApi.Contexts;
using AquarelaApi.Models;
using AquarelaApi.Repositories;
using AquarelaApi.UseCases;
using Microsoft.EntityFrameworkCore;

namespace AquarelaTest.UseCaseTests;

[TestClass]
public class UsuarioUseCaseTest
{
    private AppDbContext CriarContextoInMemory()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [TestMethod]
    public async Task GetAllAsync_DeveRetornarTodosUsuarios()
    {
        // Arrange
        var context = CriarContextoInMemory();
        context.Usuarios.AddRange(
            new Usuario { NmUsuario = "Usuario1", DsEmail = "user1@test.com", DsSenha = "senha1", DmAtivo = true },
            new Usuario { NmUsuario = "Usuario2", DsEmail = "user2@test.com", DsSenha = "senha2", DmAtivo = true }
        );
        context.SaveChanges();

        var repository = new UsuarioRepository(context);
        var useCase = new UsuarioUseCase(repository);

        // Act
        var result = await useCase.GetAllAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public async Task GetAllAsync_ComBancoVazio_DeveRetornarListaVazia()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var repository = new UsuarioRepository(context);
        var useCase = new UsuarioUseCase(repository);

        // Act
        var result = await useCase.GetAllAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count());
    }

    [TestMethod]
    public async Task GetByIdAsync_ComIdValido_DeveRetornarUsuario()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = new Usuario { NmUsuario = "Usuario Test", DsEmail = "test@test.com", DsSenha = "senha", DmAtivo = true };
        context.Usuarios.Add(usuario);
        context.SaveChanges();

        var repository = new UsuarioRepository(context);
        var useCase = new UsuarioUseCase(repository);

        // Act
        var result = await useCase.GetByIdAsync(usuario.IdUsuario);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Usuario Test", result.NmUsuario);
        Assert.AreEqual("test@test.com", result.DsEmail);
    }

    [TestMethod]
    public async Task GetByIdAsync_ComIdInvalido_DeveRetornarNull()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var repository = new UsuarioRepository(context);
        var useCase = new UsuarioUseCase(repository);

        // Act
        var result = await useCase.GetByIdAsync(999);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task CreateAsync_ComDadosValidos_DeveCriarUsuario()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var repository = new UsuarioRepository(context);
        var useCase = new UsuarioUseCase(repository);

        var novoUsuario = new Usuario
        {
            NmUsuario = "Novo Usuario",
            DsEmail = "novo@test.com",
            DsSenha = "senha123",
            DmAtivo = true
        };

        // Act
        var result = await useCase.CreateAsync(novoUsuario);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.IdUsuario > 0);
        Assert.AreEqual("Novo Usuario", result.NmUsuario);
        Assert.AreEqual("novo@test.com", result.DsEmail);
    }



    [TestMethod]
    public async Task UpdateAsync_ComDadosValidos_DeveAtualizarUsuario()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = new Usuario { NmUsuario = "Usuario Original", DsEmail = "original@test.com", DsSenha = "senha", DmAtivo = true };
        context.Usuarios.Add(usuario);
        context.SaveChanges();

        var repository = new UsuarioRepository(context);
        var useCase = new UsuarioUseCase(repository);

        usuario.NmUsuario = "Usuario Atualizado";
        usuario.DsEmail = "atualizado@test.com";

        // Act
        var result = await useCase.UpdateAsync(usuario);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Usuario Atualizado", result.NmUsuario);
        Assert.AreEqual("atualizado@test.com", result.DsEmail);
    }

    [TestMethod]
    public async Task UpdateAsync_ComIdInexistente_DeveLancarExcecao()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var repository = new UsuarioRepository(context);
        var useCase = new UsuarioUseCase(repository);

        var usuarioInexistente = new Usuario
        {
            IdUsuario = 999,
            NmUsuario = "Usuario Inexistente",
            DsEmail = "inexistente@test.com",
            DsSenha = "senha",
            DmAtivo = true
        };

        // Act & Assert - EF Core lança exceção ao tentar atualizar entidade inexistente
        bool excecaoLancada = false;
        try
        {
            await useCase.UpdateAsync(usuarioInexistente);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
        {
            excecaoLancada = true;
        }

        Assert.IsTrue(excecaoLancada, "Deve lançar DbUpdateConcurrencyException ao atualizar entidade inexistente");
    }

    [TestMethod]
    public async Task DeleteAsync_ComIdValido_DeveRemoverUsuario()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = new Usuario { NmUsuario = "Usuario Para Deletar", DsEmail = "deletar@test.com", DsSenha = "senha", DmAtivo = true };
        context.Usuarios.Add(usuario);
        context.SaveChanges();

        var repository = new UsuarioRepository(context);
        var useCase = new UsuarioUseCase(repository);

        // Act
        await useCase.DeleteAsync(usuario.IdUsuario);

        // Assert
        var usuarioRemovido = await useCase.GetByIdAsync(usuario.IdUsuario);
        Assert.IsNull(usuarioRemovido);
    }

    [TestMethod]
    public async Task DeleteAsync_ComIdInvalido_NaoDeveLancarExcecao()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var repository = new UsuarioRepository(context);
        var useCase = new UsuarioUseCase(repository);

        // Act & Assert - Não deve lançar exceção
        await useCase.DeleteAsync(999);

        // Verificar que o banco permanece vazio
        var usuarios = await useCase.GetAllAsync();
        Assert.AreEqual(0, usuarios.Count());
    }
}

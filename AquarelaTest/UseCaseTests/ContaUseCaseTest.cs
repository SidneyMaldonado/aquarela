using AquarelaApi.Contexts;
using AquarelaApi.Models;
using AquarelaApi.Repositories;
using AquarelaApi.UseCases;
using Microsoft.EntityFrameworkCore;

namespace AquarelaTest.UseCaseTests;

[TestClass]
public class ContaUseCaseTest
{
    private AppDbContext CriarContextoInMemory()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    private async Task<Usuario> CriarUsuarioTeste(AppDbContext context)
    {
        var usuario = new Usuario
        {
            NmUsuario = "Usuario Test",
            DsEmail = "test@test.com",
            DsSenha = "senha",
            DmAtivo = true
        };
        context.Usuarios.Add(usuario);
        await context.SaveChangesAsync();
        return usuario;
    }

    [TestMethod]
    public async Task GetAllAsync_DeveRetornarTodasContas()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = await CriarUsuarioTeste(context);

        context.Contas.AddRange(
            new Conta { IdUsuario = usuario.IdUsuario, NmConta = "Conta 1", NrSaldo = 1000m },
            new Conta { IdUsuario = usuario.IdUsuario, NmConta = "Conta 2", NrSaldo = 2000m }
        );
        context.SaveChanges();

        var repository = new ContaRepository(context);
        var useCase = new ContaUseCase(repository);

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
        var repository = new ContaRepository(context);
        var useCase = new ContaUseCase(repository);

        // Act
        var result = await useCase.GetAllAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count());
    }

    [TestMethod]
    public async Task GetByIdAsync_ComIdValido_DeveRetornarConta()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = await CriarUsuarioTeste(context);

        var conta = new Conta { IdUsuario = usuario.IdUsuario, NmConta = "Conta Corrente", NrSaldo = 1500.50m };
        context.Contas.Add(conta);
        context.SaveChanges();

        var repository = new ContaRepository(context);
        var useCase = new ContaUseCase(repository);

        // Act
        var result = await useCase.GetByIdAsync(conta.IdConta);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Conta Corrente", result.NmConta);
        Assert.AreEqual(1500.50m, result.NrSaldo);
    }

    [TestMethod]
    public async Task GetByIdAsync_ComIdInvalido_DeveRetornarNull()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var repository = new ContaRepository(context);
        var useCase = new ContaUseCase(repository);

        // Act
        var result = await useCase.GetByIdAsync(999);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task GetByUsuarioIdAsync_ComIdValido_DeveRetornarContasDoUsuario()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = await CriarUsuarioTeste(context);

        context.Contas.AddRange(
            new Conta { IdUsuario = usuario.IdUsuario, NmConta = "Conta 1", NrSaldo = 100m },
            new Conta { IdUsuario = usuario.IdUsuario, NmConta = "Conta 2", NrSaldo = 200m }
        );
        context.SaveChanges();

        var repository = new ContaRepository(context);
        var useCase = new ContaUseCase(repository);

        // Act
        var result = await useCase.GetByUsuarioIdAsync(usuario.IdUsuario);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
        Assert.IsTrue(result.All(c => c.IdUsuario == usuario.IdUsuario));
    }

    [TestMethod]
    public async Task GetByUsuarioIdAsync_ComUsuarioSemContas_DeveRetornarListaVazia()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = await CriarUsuarioTeste(context);

        var repository = new ContaRepository(context);
        var useCase = new ContaUseCase(repository);

        // Act
        var result = await useCase.GetByUsuarioIdAsync(usuario.IdUsuario);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count());
    }

    [TestMethod]
    public async Task CreateAsync_ComDadosValidos_DeveCriarConta()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = await CriarUsuarioTeste(context);

        var repository = new ContaRepository(context);
        var useCase = new ContaUseCase(repository);

        var novaConta = new Conta
        {
            IdUsuario = usuario.IdUsuario,
            NmConta = "Nova Conta",
            NrSaldo = 5000m
        };

        // Act
        var result = await useCase.CreateAsync(novaConta);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.IdConta > 0);
        Assert.AreEqual("Nova Conta", result.NmConta);
        Assert.AreEqual(5000m, result.NrSaldo);
    }



    [TestMethod]
    public async Task UpdateAsync_ComDadosValidos_DeveAtualizarConta()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = await CriarUsuarioTeste(context);

        var conta = new Conta { IdUsuario = usuario.IdUsuario, NmConta = "Conta Original", NrSaldo = 1000m };
        context.Contas.Add(conta);
        context.SaveChanges();

        var repository = new ContaRepository(context);
        var useCase = new ContaUseCase(repository);

        conta.NmConta = "Conta Atualizada";
        conta.NrSaldo = 2000m;

        // Act
        var result = await useCase.UpdateAsync(conta);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Conta Atualizada", result.NmConta);
        Assert.AreEqual(2000m, result.NrSaldo);
    }

    [TestMethod]
    public async Task UpdateAsync_ComIdInexistente_DeveLancarExcecao()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = await CriarUsuarioTeste(context);

        var repository = new ContaRepository(context);
        var useCase = new ContaUseCase(repository);

        var contaInexistente = new Conta
        {
            IdConta = 999,
            IdUsuario = usuario.IdUsuario,
            NmConta = "Conta Inexistente",
            NrSaldo = 500m
        };

        // Act & Assert
        bool excecaoLancada = false;
        try
        {
            await useCase.UpdateAsync(contaInexistente);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
        {
            excecaoLancada = true;
        }

        Assert.IsTrue(excecaoLancada, "Deve lançar DbUpdateConcurrencyException ao atualizar entidade inexistente");
    }

    [TestMethod]
    public async Task DeleteAsync_ComIdValido_DeveRemoverConta()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = await CriarUsuarioTeste(context);

        var conta = new Conta { IdUsuario = usuario.IdUsuario, NmConta = "Conta Para Deletar", NrSaldo = 100m };
        context.Contas.Add(conta);
        context.SaveChanges();

        var repository = new ContaRepository(context);
        var useCase = new ContaUseCase(repository);

        // Act
        await useCase.DeleteAsync(conta.IdConta);

        // Assert
        var contaRemovida = await useCase.GetByIdAsync(conta.IdConta);
        Assert.IsNull(contaRemovida);
    }

    [TestMethod]
    public async Task DeleteAsync_ComIdInvalido_NaoDeveLancarExcecao()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var repository = new ContaRepository(context);
        var useCase = new ContaUseCase(repository);

        // Act & Assert - Não deve lançar exceção
        await useCase.DeleteAsync(999);

        // Verificar que o banco permanece vazio
        var contas = await useCase.GetAllAsync();
        Assert.AreEqual(0, contas.Count());
    }
}

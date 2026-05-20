using AquarelaApi.Contexts;
using AquarelaApi.Models;
using AquarelaApi.Repositories;
using AquarelaApi.UseCases;
using Microsoft.EntityFrameworkCore;

namespace AquarelaTest.UseCaseTests;

[TestClass]
public class DividaUseCaseTest
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
    public async Task GetAllAsync_DeveRetornarTodasDividas()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = await CriarUsuarioTeste(context);

        context.Dividas.AddRange(
            new Divida
            {
                IdUsuario = usuario.IdUsuario,
                NmDivida = "Divida 1",
                DiaVencimento = 10,
                DtPrimeiroVencimento = DateTime.Now.AddMonths(1),
                NrParcelas = 12,
                NrValor = 1000m
            },
            new Divida
            {
                IdUsuario = usuario.IdUsuario,
                NmDivida = "Divida 2",
                DiaVencimento = 15,
                DtPrimeiroVencimento = DateTime.Now.AddMonths(2),
                NrParcelas = 6,
                NrValor = 500m
            }
        );
        context.SaveChanges();

        var repository = new DividaRepository(context);
        var useCase = new DividaUseCase(repository);

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
        var repository = new DividaRepository(context);
        var useCase = new DividaUseCase(repository);

        // Act
        var result = await useCase.GetAllAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count());
    }

    [TestMethod]
    public async Task GetByIdAsync_ComIdValido_DeveRetornarDivida()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = await CriarUsuarioTeste(context);

        var divida = new Divida
        {
            IdUsuario = usuario.IdUsuario,
            NmDivida = "Cartão de Crédito",
            DiaVencimento = 5,
            DtPrimeiroVencimento = new DateTime(2025, 02, 05),
            NrParcelas = 10,
            NrValor = 2500.75m
        };
        context.Dividas.Add(divida);
        context.SaveChanges();

        var repository = new DividaRepository(context);
        var useCase = new DividaUseCase(repository);

        // Act
        var result = await useCase.GetByIdAsync(divida.IdDivida);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Cartão de Crédito", result.NmDivida);
        Assert.AreEqual(2500.75m, result.NrValor);
        Assert.AreEqual(10, result.NrParcelas);
    }

    [TestMethod]
    public async Task GetByIdAsync_ComIdInvalido_DeveRetornarNull()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var repository = new DividaRepository(context);
        var useCase = new DividaUseCase(repository);

        // Act
        var result = await useCase.GetByIdAsync(999);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task GetByUsuarioIdAsync_ComIdValido_DeveRetornarDividasDoUsuario()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = await CriarUsuarioTeste(context);

        context.Dividas.AddRange(
            new Divida
            {
                IdUsuario = usuario.IdUsuario,
                NmDivida = "Divida 1",
                DiaVencimento = 10,
                DtPrimeiroVencimento = DateTime.Now,
                NrParcelas = 3,
                NrValor = 300m
            },
            new Divida
            {
                IdUsuario = usuario.IdUsuario,
                NmDivida = "Divida 2",
                DiaVencimento = 20,
                DtPrimeiroVencimento = DateTime.Now,
                NrParcelas = 5,
                NrValor = 500m
            }
        );
        context.SaveChanges();

        var repository = new DividaRepository(context);
        var useCase = new DividaUseCase(repository);

        // Act
        var result = await useCase.GetByUsuarioIdAsync(usuario.IdUsuario);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
        Assert.IsTrue(result.All(d => d.IdUsuario == usuario.IdUsuario));
    }

    [TestMethod]
    public async Task GetByUsuarioIdAsync_ComUsuarioSemDividas_DeveRetornarListaVazia()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = await CriarUsuarioTeste(context);

        var repository = new DividaRepository(context);
        var useCase = new DividaUseCase(repository);

        // Act
        var result = await useCase.GetByUsuarioIdAsync(usuario.IdUsuario);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count());
    }

    [TestMethod]
    public async Task CreateAsync_ComDadosValidos_DeveCriarDivida()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = await CriarUsuarioTeste(context);

        var repository = new DividaRepository(context);
        var useCase = new DividaUseCase(repository);

        var novaDivida = new Divida
        {
            IdUsuario = usuario.IdUsuario,
            NmDivida = "Financiamento Carro",
            DiaVencimento = 15,
            DtPrimeiroVencimento = new DateTime(2025, 03, 15),
            NrParcelas = 48,
            NrValor = 35000m
        };

        // Act
        var result = await useCase.CreateAsync(novaDivida);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.IdDivida > 0);
        Assert.AreEqual("Financiamento Carro", result.NmDivida);
        Assert.AreEqual(48, result.NrParcelas);
        Assert.AreEqual(35000m, result.NrValor);
    }



    [TestMethod]
    public async Task UpdateAsync_ComDadosValidos_DeveAtualizarDivida()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = await CriarUsuarioTeste(context);

        var divida = new Divida
        {
            IdUsuario = usuario.IdUsuario,
            NmDivida = "Divida Original",
            DiaVencimento = 10,
            DtPrimeiroVencimento = DateTime.Now,
            NrParcelas = 12,
            NrValor = 1000m
        };
        context.Dividas.Add(divida);
        context.SaveChanges();

        var repository = new DividaRepository(context);
        var useCase = new DividaUseCase(repository);

        divida.NmDivida = "Divida Atualizada";
        divida.NrValor = 2000m;
        divida.NrParcelas = 24;

        // Act
        var result = await useCase.UpdateAsync(divida);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Divida Atualizada", result.NmDivida);
        Assert.AreEqual(2000m, result.NrValor);
        Assert.AreEqual(24, result.NrParcelas);
    }

    [TestMethod]
    public async Task UpdateAsync_ComIdInexistente_DeveLancarExcecao()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = await CriarUsuarioTeste(context);

        var repository = new DividaRepository(context);
        var useCase = new DividaUseCase(repository);

        var dividaInexistente = new Divida
        {
            IdDivida = 999,
            IdUsuario = usuario.IdUsuario,
            NmDivida = "Divida Inexistente",
            DiaVencimento = 5,
            DtPrimeiroVencimento = DateTime.Now,
            NrParcelas = 6,
            NrValor = 600m
        };

        // Act & Assert
        bool excecaoLancada = false;
        try
        {
            await useCase.UpdateAsync(dividaInexistente);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
        {
            excecaoLancada = true;
        }

        Assert.IsTrue(excecaoLancada, "Deve lançar DbUpdateConcurrencyException ao atualizar entidade inexistente");
    }

    [TestMethod]
    public async Task DeleteAsync_ComIdValido_DeveRemoverDivida()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = await CriarUsuarioTeste(context);

        var divida = new Divida
        {
            IdUsuario = usuario.IdUsuario,
            NmDivida = "Divida Para Deletar",
            DiaVencimento = 10,
            DtPrimeiroVencimento = DateTime.Now,
            NrParcelas = 1,
            NrValor = 100m
        };
        context.Dividas.Add(divida);
        context.SaveChanges();

        var repository = new DividaRepository(context);
        var useCase = new DividaUseCase(repository);

        // Act
        await useCase.DeleteAsync(divida.IdDivida);

        // Assert
        var dividaRemovida = await useCase.GetByIdAsync(divida.IdDivida);
        Assert.IsNull(dividaRemovida);
    }

    [TestMethod]
    public async Task DeleteAsync_ComIdInvalido_NaoDeveLancarExcecao()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var repository = new DividaRepository(context);
        var useCase = new DividaUseCase(repository);

        // Act & Assert - Não deve lançar exceção
        await useCase.DeleteAsync(999);

        // Verificar que o banco permanece vazio
        var dividas = await useCase.GetAllAsync();
        Assert.AreEqual(0, dividas.Count());
    }
}

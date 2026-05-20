using AquarelaApi.Contexts;
using AquarelaApi.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace AquarelaTest.EndToEndTest;

[TestClass]
public class DividasEndToEndTest
{
    private static AquarelaWebFactory _factory = null!;
    private static HttpClient _client = null!;
    private static string _token = null!;
    private static int _usuarioId;
    private static int _dividaId;

    [ClassInitialize]
    public static async Task ClassInitialize(TestContext _)
    {
        _factory = new AquarelaWebFactory();
        _client = _factory.CreateClient();

        // Seed: insere o usuário e dívida de teste no banco InMemory
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var usuario = new Usuario
        {
            NmUsuario = "Admin",
            DsEmail = "admin@mail.com",
            DsSenha = "senha",
            DmAtivo = true
        };
        context.Usuarios.Add(usuario);
        context.SaveChanges();
        _usuarioId = usuario.IdUsuario;

        // Adicionar dívida de teste
        var divida = new Divida
        {
            IdUsuario = _usuarioId,
            NmDivida = "Cartão de Crédito",
            DiaVencimento = 10,
            DtPrimeiroVencimento = new DateTime(2025, 02, 10),
            NrParcelas = 12,
            NrValor = 1500.00m
        };
        context.Dividas.Add(divida);
        context.SaveChanges();
        _dividaId = divida.IdDivida;

        // Obter token JWT
        var loginPayload = new { Email = "admin@mail.com", Senha = "senha" };
        var loginResponse = await _client.PostAsJsonAsync("/api/auth/login", loginPayload);
        var loginBody = await loginResponse.Content.ReadAsStringAsync();
        var loginJson = JsonDocument.Parse(loginBody).RootElement;
        _token = loginJson.GetProperty("token").GetString()!;

        // Configurar o token no client
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [TestMethod]
    public async Task GetAll_DeveRetornar200ComListaDeDividas()
    {
        // Act
        var response = await _client.GetAsync("/api/dividas");
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
            $"Esperado 200 OK, recebido {(int)response.StatusCode}. Body: {body}");

        var json = JsonDocument.Parse(body).RootElement;
        Assert.IsTrue(json.GetArrayLength() > 0, "A lista de dívidas não deve estar vazia.");
    }

    [TestMethod]
    public async Task GetById_ComIdValido_DeveRetornar200ComDivida()
    {
        // Act
        var response = await _client.GetAsync($"/api/dividas/{_dividaId}");
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
            $"Esperado 200 OK, recebido {(int)response.StatusCode}. Body: {body}");

        var json = JsonDocument.Parse(body).RootElement;
        Assert.AreEqual(_dividaId, json.GetProperty("idDivida").GetInt32());
        Assert.AreEqual("Cartão de Crédito", json.GetProperty("nmDivida").GetString());
        Assert.AreEqual(1500.00m, json.GetProperty("nrValor").GetDecimal());
    }

    [TestMethod]
    public async Task GetById_ComIdInvalido_DeveRetornar404()
    {
        // Act
        var response = await _client.GetAsync("/api/dividas/9999");

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode,
            $"Esperado 404 NotFound, recebido {(int)response.StatusCode}.");
    }

    [TestMethod]
    public async Task GetByUsuario_ComIdUsuarioValido_DeveRetornar200ComListaDeDividas()
    {
        // Act
        var response = await _client.GetAsync($"/api/dividas/usuario/{_usuarioId}");
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
            $"Esperado 200 OK, recebido {(int)response.StatusCode}. Body: {body}");

        var json = JsonDocument.Parse(body).RootElement;
        Assert.IsTrue(json.GetArrayLength() > 0, "A lista de dívidas do usuário não deve estar vazia.");
    }

    [TestMethod]
    public async Task Create_ComDadosValidos_DeveRetornar201ComDividaCriada()
    {
        // Arrange
        var payload = new
        {
            IdUsuario = _usuarioId,
            NmDivida = "Financiamento Carro",
            DiaVencimento = 15,
            DtPrimeiroVencimento = new DateTime(2025, 03, 15),
            NrParcelas = 48,
            NrValor = 25000.00m
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/dividas", payload);
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode,
            $"Esperado 201 Created, recebido {(int)response.StatusCode}. Body: {body}");

        var json = JsonDocument.Parse(body).RootElement;
        Assert.IsTrue(json.GetProperty("idDivida").GetInt32() > 0);
        Assert.AreEqual("Financiamento Carro", json.GetProperty("nmDivida").GetString());
        Assert.AreEqual(25000.00m, json.GetProperty("nrValor").GetDecimal());
    }

    [TestMethod]
    public async Task Update_ComDadosValidos_DeveRetornar200ComDividaAtualizada()
    {
        // Arrange - Criar uma nova dívida para atualizar
        var createPayload = new
        {
            IdUsuario = _usuarioId,
            NmDivida = "Dívida Para Atualizar",
            DiaVencimento = 5,
            DtPrimeiroVencimento = new DateTime(2025, 05, 05),
            NrParcelas = 6,
            NrValor = 800.00m
        };
        var createResponse = await _client.PostAsJsonAsync("/api/dividas", createPayload);
        var createBody = await createResponse.Content.ReadAsStringAsync();
        var createJson = JsonDocument.Parse(createBody).RootElement;
        var dividaId = createJson.GetProperty("idDivida").GetInt32();

        var updatePayload = new
        {
            IdDivida = dividaId,
            IdUsuario = _usuarioId,
            NmDivida = "Dívida Atualizada",
            DiaVencimento = 10,
            DtPrimeiroVencimento = new DateTime(2025, 02, 10),
            NrParcelas = 12,
            NrValor = 2000.00m
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/dividas/{dividaId}", updatePayload);
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
            $"Esperado 200 OK, recebido {(int)response.StatusCode}. Body: {body}");

        var json = JsonDocument.Parse(body).RootElement;
        Assert.AreEqual("Dívida Atualizada", json.GetProperty("nmDivida").GetString());
        Assert.AreEqual(2000.00m, json.GetProperty("nrValor").GetDecimal());
    }

    [TestMethod]
    public async Task Update_ComIdDivergente_DeveRetornar400()
    {
        // Arrange
        var payload = new
        {
            IdDivida = 1,
            IdUsuario = _usuarioId,
            NmDivida = "Dívida Teste",
            DiaVencimento = 10,
            DtPrimeiroVencimento = new DateTime(2025, 02, 10),
            NrParcelas = 12,
            NrValor = 1000.00m
        };

        // Act
        var response = await _client.PutAsJsonAsync("/api/dividas/999", payload);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode,
            $"Esperado 400 BadRequest, recebido {(int)response.StatusCode}.");
    }

    [TestMethod]
    public async Task Delete_ComIdValido_DeveRetornar204()
    {
        // Arrange - Criar uma dívida para deletar
        var createPayload = new
        {
            IdUsuario = _usuarioId,
            NmDivida = "Dívida Para Deletar",
            DiaVencimento = 20,
            DtPrimeiroVencimento = new DateTime(2025, 04, 20),
            NrParcelas = 6,
            NrValor = 500.00m
        };
        var createResponse = await _client.PostAsJsonAsync("/api/dividas", createPayload);
        var createBody = await createResponse.Content.ReadAsStringAsync();
        var createJson = JsonDocument.Parse(createBody).RootElement;
        var dividaId = createJson.GetProperty("idDivida").GetInt32();

        // Act
        var response = await _client.DeleteAsync($"/api/dividas/{dividaId}");

        // Assert
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode,
            $"Esperado 204 NoContent, recebido {(int)response.StatusCode}.");
    }
}

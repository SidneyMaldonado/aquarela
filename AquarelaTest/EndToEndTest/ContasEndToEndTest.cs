using AquarelaApi.Contexts;
using AquarelaApi.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace AquarelaTest.EndToEndTest;

[TestClass]
public class ContasEndToEndTest
{
    private static AquarelaWebFactory _factory = null!;
    private static HttpClient _client = null!;
    private static string _token = null!;
    private static int _usuarioId;
    private static int _contaId;

    [ClassInitialize]
    public static async Task ClassInitialize(TestContext _)
    {
        _factory = new AquarelaWebFactory();
        _client = _factory.CreateClient();

        // Seed: insere o usuário e conta de teste no banco InMemory
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

        // Adicionar conta de teste
        var conta = new Conta
        {
            IdUsuario = _usuarioId,
            NmConta = "Conta Corrente",
            NrSaldo = 1000.50m
        };
        context.Contas.Add(conta);
        context.SaveChanges();
        _contaId = conta.IdConta;

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
    public async Task GetAll_DeveRetornar200ComListaDeContas()
    {
        // Act
        var response = await _client.GetAsync("/api/contas");
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
            $"Esperado 200 OK, recebido {(int)response.StatusCode}. Body: {body}");

        var json = JsonDocument.Parse(body).RootElement;
        Assert.IsTrue(json.GetArrayLength() > 0, "A lista de contas não deve estar vazia.");
    }

    [TestMethod]
    public async Task GetById_ComIdValido_DeveRetornar200ComConta()
    {
        // Act
        var response = await _client.GetAsync($"/api/contas/{_contaId}");
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
            $"Esperado 200 OK, recebido {(int)response.StatusCode}. Body: {body}");

        var json = JsonDocument.Parse(body).RootElement;
        Assert.AreEqual(_contaId, json.GetProperty("idConta").GetInt32());
        Assert.AreEqual("Conta Corrente", json.GetProperty("nmConta").GetString());
        Assert.AreEqual(1000.50m, json.GetProperty("nrSaldo").GetDecimal());
    }

    [TestMethod]
    public async Task GetById_ComIdInvalido_DeveRetornar404()
    {
        // Act
        var response = await _client.GetAsync("/api/contas/9999");

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode,
            $"Esperado 404 NotFound, recebido {(int)response.StatusCode}.");
    }

    [TestMethod]
    public async Task GetByUsuario_ComIdUsuarioValido_DeveRetornar200ComListaDeContas()
    {
        // Act
        var response = await _client.GetAsync($"/api/contas/usuario/{_usuarioId}");
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
            $"Esperado 200 OK, recebido {(int)response.StatusCode}. Body: {body}");

        var json = JsonDocument.Parse(body).RootElement;
        Assert.IsTrue(json.GetArrayLength() > 0, "A lista de contas do usuário não deve estar vazia.");
    }

    [TestMethod]
    public async Task Create_ComDadosValidos_DeveRetornar201ComContaCriada()
    {
        // Arrange
        var payload = new
        {
            IdUsuario = _usuarioId,
            NmConta = "Conta Poupança",
            NrSaldo = 5000.00m
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/contas", payload);
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode,
            $"Esperado 201 Created, recebido {(int)response.StatusCode}. Body: {body}");

        var json = JsonDocument.Parse(body).RootElement;
        Assert.IsTrue(json.GetProperty("idConta").GetInt32() > 0);
        Assert.AreEqual("Conta Poupança", json.GetProperty("nmConta").GetString());
        Assert.AreEqual(5000.00m, json.GetProperty("nrSaldo").GetDecimal());
    }

    [TestMethod]
    public async Task Update_ComDadosValidos_DeveRetornar200ComContaAtualizada()
    {
        // Arrange - Criar uma nova conta para atualizar
        var createPayload = new
        {
            IdUsuario = _usuarioId,
            NmConta = "Conta Para Atualizar",
            NrSaldo = 500.00m
        };
        var createResponse = await _client.PostAsJsonAsync("/api/contas", createPayload);
        var createBody = await createResponse.Content.ReadAsStringAsync();
        var createJson = JsonDocument.Parse(createBody).RootElement;
        var contaId = createJson.GetProperty("idConta").GetInt32();

        var updatePayload = new
        {
            IdConta = contaId,
            IdUsuario = _usuarioId,
            NmConta = "Conta Atualizada",
            NrSaldo = 2000.75m
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/contas/{contaId}", updatePayload);
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
            $"Esperado 200 OK, recebido {(int)response.StatusCode}. Body: {body}");

        var json = JsonDocument.Parse(body).RootElement;
        Assert.AreEqual("Conta Atualizada", json.GetProperty("nmConta").GetString());
        Assert.AreEqual(2000.75m, json.GetProperty("nrSaldo").GetDecimal());
    }

    [TestMethod]
    public async Task Update_ComIdDivergente_DeveRetornar400()
    {
        // Arrange
        var payload = new
        {
            IdConta = 1,
            IdUsuario = _usuarioId,
            NmConta = "Conta Teste",
            NrSaldo = 1000.00m
        };

        // Act
        var response = await _client.PutAsJsonAsync("/api/contas/999", payload);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode,
            $"Esperado 400 BadRequest, recebido {(int)response.StatusCode}.");
    }

    [TestMethod]
    public async Task Delete_ComIdValido_DeveRetornar204()
    {
        // Arrange - Criar uma conta para deletar
        var createPayload = new
        {
            IdUsuario = _usuarioId,
            NmConta = "Conta Para Deletar",
            NrSaldo = 100.00m
        };
        var createResponse = await _client.PostAsJsonAsync("/api/contas", createPayload);
        var createBody = await createResponse.Content.ReadAsStringAsync();
        var createJson = JsonDocument.Parse(createBody).RootElement;
        var contaId = createJson.GetProperty("idConta").GetInt32();

        // Act
        var response = await _client.DeleteAsync($"/api/contas/{contaId}");

        // Assert
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode,
            $"Esperado 204 NoContent, recebido {(int)response.StatusCode}.");
    }
}

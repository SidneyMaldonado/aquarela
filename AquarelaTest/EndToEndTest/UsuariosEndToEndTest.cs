using AquarelaApi.Contexts;
using AquarelaApi.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace AquarelaTest.EndToEndTest;

[TestClass]
public class UsuariosEndToEndTest
{
    private static AquarelaWebFactory _factory = null!;
    private static HttpClient _client = null!;
    private static string _token = null!;
    private static int _usuarioId;

    [ClassInitialize]
    public static async Task ClassInitialize(TestContext _)
    {
        _factory = new AquarelaWebFactory();
        _client = _factory.CreateClient();

        // Seed: insere o usuário de teste no banco InMemory
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
    public async Task GetAll_DeveRetornar200ComListaDeUsuarios()
    {
        // Act
        var response = await _client.GetAsync("/api/usuarios");
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
            $"Esperado 200 OK, recebido {(int)response.StatusCode}. Body: {body}");

        var json = JsonDocument.Parse(body).RootElement;
        Assert.IsTrue(json.GetArrayLength() > 0, "A lista de usuários não deve estar vazia.");
    }

    [TestMethod]
    public async Task GetById_ComIdValido_DeveRetornar200ComUsuario()
    {
        // Act
        var response = await _client.GetAsync($"/api/usuarios/{_usuarioId}");
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
            $"Esperado 200 OK, recebido {(int)response.StatusCode}. Body: {body}");

        var json = JsonDocument.Parse(body).RootElement;
        Assert.AreEqual(_usuarioId, json.GetProperty("idUsuario").GetInt32());
        Assert.AreEqual("Admin", json.GetProperty("nmUsuario").GetString());
    }

    [TestMethod]
    public async Task GetById_ComIdInvalido_DeveRetornar404()
    {
        // Act
        var response = await _client.GetAsync("/api/usuarios/9999");

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode,
            $"Esperado 404 NotFound, recebido {(int)response.StatusCode}.");
    }

    [TestMethod]
    public async Task Create_ComDadosValidos_DeveRetornar201ComUsuarioCriado()
    {
        // Arrange
        var payload = new
        {
            NmUsuario = "Novo Usuario",
            DsEmail = "novo@mail.com",
            DsSenha = "senha123",
            DmAtivo = true
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/usuarios", payload);
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode,
            $"Esperado 201 Created, recebido {(int)response.StatusCode}. Body: {body}");

        var json = JsonDocument.Parse(body).RootElement;
        Assert.IsTrue(json.GetProperty("idUsuario").GetInt32() > 0);
        Assert.AreEqual("Novo Usuario", json.GetProperty("nmUsuario").GetString());
        Assert.AreEqual("novo@mail.com", json.GetProperty("dsEmail").GetString());
        Assert.IsFalse(json.TryGetProperty("dsSenha", out _), "A senha não deve ser retornada.");
    }

    [TestMethod]
    public async Task Update_ComDadosValidos_DeveRetornar200ComUsuarioAtualizado()
    {
        // Arrange - Criar um novo usuário para atualizar
        var createPayload = new
        {
            NmUsuario = "Usuario Para Atualizar",
            DsEmail = "atualizar@mail.com",
            DsSenha = "senha123",
            DmAtivo = true
        };
        var createResponse = await _client.PostAsJsonAsync("/api/usuarios", createPayload);
        var createBody = await createResponse.Content.ReadAsStringAsync();
        var createJson = JsonDocument.Parse(createBody).RootElement;
        var usuarioId = createJson.GetProperty("idUsuario").GetInt32();

        var updatePayload = new
        {
            IdUsuario = usuarioId,
            NmUsuario = "Usuario Atualizado",
            DsEmail = "atualizar@mail.com",
            DmAtivo = true
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/usuarios/{usuarioId}", updatePayload);
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
            $"Esperado 200 OK, recebido {(int)response.StatusCode}. Body: {body}");

        var json = JsonDocument.Parse(body).RootElement;
        Assert.AreEqual("Usuario Atualizado", json.GetProperty("nmUsuario").GetString());
    }

    [TestMethod]
    public async Task Update_ComIdDivergente_DeveRetornar400()
    {
        // Arrange
        var payload = new
        {
            IdUsuario = 1,
            NmUsuario = "Admin",
            DsEmail = "admin@mail.com",
            DmAtivo = true
        };

        // Act
        var response = await _client.PutAsJsonAsync("/api/usuarios/999", payload);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode,
            $"Esperado 400 BadRequest, recebido {(int)response.StatusCode}.");
    }

    [TestMethod]
    public async Task Delete_ComIdValido_DeveRetornar204()
    {
        // Arrange - Criar um usuário para deletar
        var createPayload = new
        {
            NmUsuario = "Usuario Para Deletar",
            DsEmail = "deletar@mail.com",
            DsSenha = "senha123",
            DmAtivo = true
        };
        var createResponse = await _client.PostAsJsonAsync("/api/usuarios", createPayload);
        var createBody = await createResponse.Content.ReadAsStringAsync();
        var createJson = JsonDocument.Parse(createBody).RootElement;
        var userId = createJson.GetProperty("idUsuario").GetInt32();

        // Act
        var response = await _client.DeleteAsync($"/api/usuarios/{userId}");

        // Assert
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode,
            $"Esperado 204 NoContent, recebido {(int)response.StatusCode}.");
    }
}

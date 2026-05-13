using AquarelaApi.Contexts;
using AquarelaApi.Models;
using AquarelaApi.Repositories;
using AquarelaApi.UseCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AquarelaTest
{
    [TestClass]
    public sealed class LoginTest
    {
        private AppDbContext CriarContextoInMemory()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            context.Usuarios.Add(new Usuario
            {
                IdUsuario = 1,
                NmUsuario = "Admin",
                DsEmail = "admin@mail.com",
                DsSenha = "senha",
                DmAtivo = true
            });

            context.SaveChanges();
            return context;
        }

        private IConfiguration CriarConfiguration()
        {
            var config = new Dictionary<string, string?>
            {
                { "Jwt:Key",      "AquarelaSecretKey@2026!MustBe32Chars!" },
                { "Jwt:Issuer",   "AquarelaApi" },
                { "Jwt:Audience", "AquarelaApi" }
            };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(config)
                .Build();
        }

        [TestMethod]
        public async Task LoginTest_Sucess()
        {
            // Arrange
            var context = CriarContextoInMemory();
            var repository = new UsuarioRepository(context);
            var configuration = CriarConfiguration();
            var useCase = new LoginUseCase(repository, configuration);

            // Act
            var token = await useCase.ExecuteAsync("admin@mail.com", "senha");

            // Assert
            Assert.IsNotNull(token, "O token não deve ser nulo para credenciais válidas.");
            Assert.IsTrue(token.Length > 0, "O token deve ser uma string não vazia.");
        }
    }
}


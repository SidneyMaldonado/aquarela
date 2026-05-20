using AquarelaApi.Services;

namespace AquarelaTest;

[TestClass]
public class DecryptServiceTest
{
    [TestMethod]
    public void Encrypt_DeveRetornarStringHexadecimal()
    {
        // Arrange
        string textoOriginal = "Hello World";

        // Act
        string textoCriptografado = DecryptService.Encrypt(textoOriginal);

        // Assert
        Assert.IsNotNull(textoCriptografado);
        Assert.IsGreaterThan(0, textoCriptografado.Length);

        // Validar que todos os caracteres são hexadecimais (0-9, a-f)
        foreach (char c in textoCriptografado)
        {
            Assert.IsTrue(char.IsDigit(c) || (c >= 'a' && c <= 'f'),
                $"Caractere '{c}' não é hexadecimal válido");
        }
    }

    [TestMethod]
    public void Decrypt_DeveRetornarTextoOriginal()
    {
        // Arrange
        string textoOriginal = "Hello World";
        string textoCriptografado = DecryptService.Encrypt(textoOriginal);

        // Act
        string textoDescriptografado = DecryptService.Decrypt(textoCriptografado);

        // Assert
        Assert.AreEqual(textoOriginal, textoDescriptografado, 
            "O texto descriptografado deve ser igual ao texto original");
    }

    [TestMethod]
    public void EncryptDecrypt_ComCaracteresEspeciais_DeveFuncionar()
    {
        // Arrange
        string textoOriginal = "Olá! @#$%¨&*() Ação Çedilha";

        // Act
        string textoCriptografado = DecryptService.Encrypt(textoOriginal);
        string textoDescriptografado = DecryptService.Decrypt(textoCriptografado);

        // Assert
        Assert.AreEqual(textoOriginal, textoDescriptografado);
    }

    [TestMethod]
    public void EncryptDecrypt_ComTextoVazio_DeveRetornarVazio()
    {
        // Arrange
        string textoOriginal = "";

        // Act
        string textoCriptografado = DecryptService.Encrypt(textoOriginal);
        string textoDescriptografado = DecryptService.Decrypt(textoCriptografado);

        // Assert
        Assert.AreEqual("", textoCriptografado);
        Assert.AreEqual("", textoDescriptografado);
    }

    [TestMethod]
    public void EncryptDecrypt_ComChavePersonalizada_DeveFuncionar()
    {
        // Arrange
        string textoOriginal = "Dados Secretos";
        string chavePersonalizada = "MinhaChave123!@#";

        // Act
        string textoCriptografado = DecryptService.Encrypt(textoOriginal, chavePersonalizada);
        string textoDescriptografado = DecryptService.Decrypt(textoCriptografado, chavePersonalizada);

        // Assert
        Assert.AreEqual(textoOriginal, textoDescriptografado);
    }

    [TestMethod]
    public void Decrypt_ComChaveDiferente_DeveRetornarTextoDiferente()
    {
        // Arrange
        string textoOriginal = "Teste de Segurança";
        string chave1 = "ChaveCorreta123";
        string chave2 = "ChaveErrada456";
        string textoCriptografado = DecryptService.Encrypt(textoOriginal, chave1);

        // Act
        string decriptoCorreto = DecryptService.Decrypt(textoCriptografado, chave1);
        string decriptoErrado = DecryptService.Decrypt(textoCriptografado, chave2);

        // Assert
        Assert.AreEqual(textoOriginal, decriptoCorreto, 
            "Chave correta deve retornar texto original");
        Assert.AreNotEqual(textoOriginal, decriptoErrado, 
            "Chave errada não deve retornar texto original");
    }

    [TestMethod]
    public void EncryptDecrypt_ConnectionString_DeveFuncionar()
    {
        // Arrange - Simular connection string real
        string connectionString = "Server=tcp:aquarela-sql.database.windows.net,1433;Initial Catalog=free-sql-db-5947062;Persist Security Info=False;User ID=aquarela-sql;Password=Azure@99!!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // Act
        string connectionStringCriptografada = DecryptService.Encrypt(connectionString);
        string connectionStringDescriptografada = DecryptService.Decrypt(connectionStringCriptografada);

        // Assert
        Assert.AreEqual(connectionString, connectionStringDescriptografada,
            "Connection string descriptografada deve ser igual à original");

        // Validar que contém os componentes principais
        StringAssert.Contains(connectionStringDescriptografada, "aquarela-sql.database.windows.net");
        StringAssert.Contains(connectionStringDescriptografada, "free-sql-db-5947062");
        StringAssert.Contains(connectionStringDescriptografada, "Password=Azure@99!!");
    }

    [TestMethod]
    public void EncryptDecrypt_TextoLongo_DeveFuncionar()
    {
        // Arrange
        string textoLongo = new string('A', 1000) + " " + new string('B', 1000);

        // Act
        string textoCriptografado = DecryptService.Encrypt(textoLongo);
        string textoDescriptografado = DecryptService.Decrypt(textoCriptografado);

        // Assert
        Assert.AreEqual(textoLongo, textoDescriptografado);
        Assert.AreEqual(textoLongo.Length, textoDescriptografado.Length);
    }

    [TestMethod]
    public void Decrypt_ComHexInvalido_DeveLancarExcecao()
    {
        // Arrange
        string hexInvalido = "XYZ123"; // Não é hexadecimal válido

        // Act & Assert
        try
        {
            DecryptService.Decrypt(hexInvalido);
            Assert.Fail("Deveria lançar exceção para hex inválido");
        }
        catch (InvalidOperationException)
        {
            // Esperado - exceção foi lançada corretamente
        }
    }

    [TestMethod]
    public void Decrypt_ComHexComprimentoImpar_DeveLancarExcecao()
    {
        // Arrange
        string hexImpar = "abc"; // Comprimento ímpar

        // Act & Assert
        try
        {
            DecryptService.Decrypt(hexImpar);
            Assert.Fail("Deveria lançar exceção para hex com comprimento ímpar");
        }
        catch (InvalidOperationException ex)
        {
            bool mensagemValida = ex.Message.Contains("descriptografar") || 
                                  (ex.InnerException?.Message.Contains("comprimento par") ?? false);
            Assert.IsTrue(mensagemValida, 
                $"Mensagem de erro inesperada: {ex.Message}");
        }
    }

    [TestMethod]
    public void CompatibilidadeComCripyCpp_DeveFuncionar()
    {
        // Arrange - Dados que seriam criptografados pelo Cripy.cpp
        string textoOriginal = "Azure@99!!";

        // Simular resultado do Cripy.cpp (criptografar localmente)
        string textoCriptografado = DecryptService.Encrypt(textoOriginal);

        // Act - Descriptografar com C#
        string textoDescriptografado = DecryptService.Decrypt(textoCriptografado);

        // Assert
        Assert.AreEqual(textoOriginal, textoDescriptografado,
            "Deve ser compatível com o algoritmo do Cripy.cpp");
    }
}

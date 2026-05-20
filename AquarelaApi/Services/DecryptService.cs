using System.Text;

namespace AquarelaApi.Services;

/// <summary>
/// Serviço de descriptografia usando algoritmo XOR (compatível com Cripy.cpp)
/// </summary>
public static class DecryptService
{
    private const string ChavePadrao = "AquarelaKey2026";

    /// <summary>
    /// Descriptografa uma string hexadecimal usando XOR com chave
    /// </summary>
    /// <param name="textoHex">String criptografada em formato hexadecimal</param>
    /// <param name="chave">Chave de descriptografia (padrão: "AquarelaKey2026")</param>
    /// <returns>String descriptografada (texto original)</returns>
    public static string Decrypt(string textoHex, string? chave = null)
    {
        if (string.IsNullOrWhiteSpace(textoHex))
            return string.Empty;

        chave ??= ChavePadrao;

        try
        {
            // Converter hexadecimal de volta para bytes
            var textoCriptografado = HexToBytes(textoHex);

            // Descriptografar usando XOR com a mesma chave
            var resultado = new StringBuilder();
            int chaveLen = chave.Length;

            for (int i = 0; i < textoCriptografado.Length; i++)
            {
                // XOR de cada byte com a chave (rotação circular)
                char c = (char)(textoCriptografado[i] ^ chave[i % chaveLen]);
                resultado.Append(c);
            }

            return resultado.ToString();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Erro ao descriptografar: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Criptografa uma string usando XOR com chave
    /// </summary>
    /// <param name="texto">String a ser criptografada</param>
    /// <param name="chave">Chave de criptografia (padrão: "AquarelaKey2026")</param>
    /// <returns>String criptografada em formato hexadecimal</returns>
    public static string Encrypt(string texto, string? chave = null)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return string.Empty;

        chave ??= ChavePadrao;

        try
        {
            var bytes = new List<byte>();
            int chaveLen = chave.Length;

            // XOR de cada caractere com a chave
            for (int i = 0; i < texto.Length; i++)
            {
                byte c = (byte)(texto[i] ^ chave[i % chaveLen]);
                bytes.Add(c);
            }

            // Converter para hexadecimal
            return BytesToHex(bytes.ToArray());
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Erro ao criptografar: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Converte string hexadecimal para array de bytes
    /// </summary>
    private static byte[] HexToBytes(string hex)
    {
        if (hex.Length % 2 != 0)
            throw new ArgumentException("String hexadecimal deve ter comprimento par");

        byte[] bytes = new byte[hex.Length / 2];
        for (int i = 0; i < hex.Length; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }
        return bytes;
    }

    /// <summary>
    /// Converte array de bytes para string hexadecimal
    /// </summary>
    private static string BytesToHex(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }
}

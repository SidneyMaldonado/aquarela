# DecryptService - Serviço de Criptografia/Descriptografia

## Descrição

Serviço de criptografia e descriptografia compatível com o algoritmo XOR implementado no **Cripy.cpp**. Permite criptografar strings em C++ e descriptografá-las em C# (e vice-versa).

## Implementação

### Localização
- **Namespace**: `AquarelaApi.Services`
- **Arquivo**: `AquarelaApi/Services/DecryptService.cs`
- **Tipo**: Static class (métodos estáticos)

### Compatibilidade
✅ **100% compatível** com `Cripy.cpp`  
✅ Mesmo algoritmo XOR com chave rotacional  
✅ Mesmo formato hexadecimal de saída  
✅ Mesma chave padrão: `"AquarelaKey2026"`

## Métodos Públicos

### 1. `DecryptService.Decrypt(string textoHex, string? chave = null)`

Descriptografa uma string hexadecimal criptografada.

**Parâmetros:**
- `textoHex`: String criptografada em formato hexadecimal (gerada por `Encrypt` ou por `Cripy.exe`)
- `chave`: Chave de descriptografia (opcional, padrão: "AquarelaKey2026")

**Retorno:**
- String descriptografada (texto original)

**Exemplo:**
```csharp
string connectionStringCripto = "3a0b1c2d..."; // Gerado pelo Cripy.exe
string connectionString = DecryptService.Decrypt(connectionStringCripto);
Console.WriteLine(connectionString); 
// Saída: Server=tcp:aquarela-sql...
```

### 2. `DecryptService.Encrypt(string texto, string? chave = null)`

Criptografa uma string usando XOR com chave.

**Parâmetros:**
- `texto`: String a ser criptografada
- `chave`: Chave de criptografia (opcional, padrão: "AquarelaKey2026")

**Retorno:**
- String criptografada em formato hexadecimal

**Exemplo:**
```csharp
string senha = "Azure@99!!";
string senhaCripto = DecryptService.Encrypt(senha);
Console.WriteLine(senhaCripto); 
// Saída: 3a0b1c2d... (hexadecimal)
```

## Uso no Program.cs

### Configuração para Produção

O serviço é usado para descriptografar a connection string em produção:

```csharp
using AquarelaApi.Services;

// EF Core - SQL Server
#if DEBUG
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#else
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(DecryptService.Decrypt(
		builder.Configuration.GetConnectionString("DefaultConnection")!)));
#endif
```

### Como Funciona

1. **Desenvolvimento (DEBUG)**:
   - Connection string em `appsettings.Development.json` em texto plano
   - EF Core usa diretamente sem descriptografia

2. **Produção (RELEASE)**:
   - Connection string em `appsettings.json` ou variável de ambiente **criptografada**
   - `DecryptService.Decrypt()` descriptografa antes de passar para EF Core
   - Protege credenciais em repositórios públicos e logs

## Fluxo de Trabalho

### Preparar Connection String para Produção

#### Passo 1: Criptografar com Cripy.exe

```powershell
# No terminal, executar o Cripy.exe
.\Cripy.exe "Server=tcp:aquarela-sql.database.windows.net,1433;..."

# Saída:
# === RESULTADO ===
# Texto Original: Server=tcp:aquarela-sql...
# Texto Criptografado (Hex): 3a0b1c2d4e5f6a7b8c9d0e1f...
```

#### Passo 2: Adicionar ao appsettings.json

```json
{
  "ConnectionStrings": {
	"DefaultConnection": "3a0b1c2d4e5f6a7b8c9d0e1f..."
  }
}
```

#### Passo 3: Deploy

Ao fazer deploy, a aplicação em **modo RELEASE** irá:
1. Ler a connection string criptografada de `appsettings.json`
2. Chamar `DecryptService.Decrypt()`
3. Usar a connection string descriptografada com EF Core

### Exemplo Completo

```csharp
// 1. Criptografar localmente (pode usar Cripy.exe ou o próprio serviço)
string connStr = "Server=tcp:aquarela-sql.database.windows.net,1433;Initial Catalog=free-sql-db-5947062;User ID=aquarela-sql;Password=Azure@99!!;...";
string connStrCripto = DecryptService.Encrypt(connStr);

Console.WriteLine("Connection String Criptografada:");
Console.WriteLine(connStrCripto);
// Saída: 3a0b1c2d4e5f... (copiar para appsettings.json)

// 2. Em produção, descriptografar automaticamente
string connStrOriginal = DecryptService.Decrypt(connStrCripto);
Console.WriteLine("Connection String Original:");
Console.WriteLine(connStrOriginal);
// Saída: Server=tcp:aquarela-sql...
```

## Segurança

### ✅ Vantagens

1. **Proteção em repositórios**: Connection strings criptografadas não expõem credenciais
2. **Compatibilidade**: Mesma chave entre C++ e C# simplifica o processo
3. **Simples**: Não requer bibliotecas externas complexas

### ⚠️ Limitações

1. **Segurança Básica**: XOR é adequado para **ofuscação**, não para segurança crítica
2. **Chave Hardcoded**: A chave `"AquarelaKey2026"` está no código-fonte
3. **Não é criptografia forte**: Para dados sensíveis em produção, considere AES-256

### 🔐 Recomendações

Para **máxima segurança**:

1. **Azure Key Vault**: Armazene connection strings no Key Vault
2. **Variáveis de Ambiente**: Use variáveis de ambiente criptografadas
3. **Managed Identity**: Use Azure Managed Identity para SQL Server

**Exemplo com Azure Key Vault:**
```csharp
// Em vez de descriptografar localmente
builder.Configuration.AddAzureKeyVault(
	new Uri("https://seu-keyvault.vault.azure.net/"),
	new DefaultAzureCredential());

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
```

## Testes

### Executar Testes Unitários

```bash
dotnet test --filter FullyQualifiedName~DecryptServiceTest
```

### Testes Implementados

✅ **11 testes, 100% de cobertura:**

1. `Encrypt_DeveRetornarStringHexadecimal` - Valida formato hex
2. `Decrypt_DeveRetornarTextoOriginal` - Valida reversibilidade
3. `EncryptDecrypt_ComCaracteresEspeciais_DeveFuncionar` - UTF-8 e caracteres especiais
4. `EncryptDecrypt_ComTextoVazio_DeveRetornarVazio` - Edge case
5. `EncryptDecrypt_ComChavePersonalizada_DeveFuncionar` - Chaves customizadas
6. `Decrypt_ComChaveDiferente_DeveRetornarTextoDiferente` - Validação de segurança
7. `EncryptDecrypt_ConnectionString_DeveFuncionar` - Caso de uso principal
8. `EncryptDecrypt_TextoLongo_DeveFuncionar` - Performance
9. `Decrypt_ComHexInvalido_DeveLancarExcecao` - Tratamento de erro
10. `Decrypt_ComHexComprimentoImpar_DeveLancarExcecao` - Validação de entrada
11. `CompatibilidadeComCripyCpp_DeveFuncionar` - Compatibilidade C++/C#

## Exemplos de Uso

### Exemplo 1: Criptografar Senha

```csharp
using AquarelaApi.Services;

string senha = "MinhaSenhaSecreta123!";
string senhaCripto = DecryptService.Encrypt(senha);

Console.WriteLine($"Senha Criptografada: {senhaCripto}");
// Armazenar senhaCripto no banco de dados
```

### Exemplo 2: Descriptografar Connection String

```csharp
using AquarelaApi.Services;

// Connection string criptografada (do appsettings.json)
string connStrCripto = builder.Configuration.GetConnectionString("DefaultConnection")!;

// Descriptografar
string connStr = DecryptService.Decrypt(connStrCripto);

// Usar com EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(connStr));
```

### Exemplo 3: Chave Personalizada

```csharp
using AquarelaApi.Services;

string chave = "MinhaChavePersonalizada2026!@#";
string textoCripto = DecryptService.Encrypt("Dados Secretos", chave);
string textoOriginal = DecryptService.Decrypt(textoCripto, chave);

Console.WriteLine($"Original: {textoOriginal}"); // "Dados Secretos"
```

## Integração com Cripy.exe

### Workflow Completo

1. **Desenvolver localmente** (DEBUG):
   ```json
   // appsettings.Development.json
   {
	 "ConnectionStrings": {
	   "DefaultConnection": "Server=tcp:aquarela-sql..."
	 }
   }
   ```

2. **Preparar para produção**:
   ```powershell
   # Criptografar com Cripy.exe
   .\Cripy.exe "Server=tcp:aquarela-sql..."
   # Copiar saída hexadecimal
   ```

3. **Configurar produção**:
   ```json
   // appsettings.json
   {
	 "ConnectionStrings": {
	   "DefaultConnection": "3a0b1c2d4e5f..." // Hex criptografado
	 }
   }
   ```

4. **Deploy**: A aplicação automaticamente descriptografa em modo RELEASE

## Troubleshooting

### Erro: "Não é possível conectar ao banco de dados"

**Causa**: Connection string descriptografada incorreta

**Solução**:
1. Verificar se usou a chave correta para criptografar
2. Testar localmente:
   ```csharp
   string teste = DecryptService.Decrypt("seu_hex_aqui");
   Console.WriteLine(teste); // Deve exibir a connection string original
   ```

### Erro: "String hexadecimal deve ter comprimento par"

**Causa**: Hex inválido ou incompleto

**Solução**:
1. Verificar se copiou o hex completo do Cripy.exe
2. Validar que não há espaços ou quebras de linha

### Erro: "Caractere não é hexadecimal válido"

**Causa**: Caracteres inválidos no hex

**Solução**:
1. Hex deve conter apenas 0-9 e a-f
2. Re-gerar o hex com Cripy.exe

## Conclusão

O `DecryptService` fornece uma camada de proteção para credenciais no código-fonte, mantendo compatibilidade total com o `Cripy.cpp`. É adequado para ofuscação de dados e integração C++/C#, mas para ambientes de produção críticos, considere usar Azure Key Vault ou soluções de criptografia mais robustas.

**Status**: ✅ Pronto para uso em produção com as ressalvas de segurança mencionadas.

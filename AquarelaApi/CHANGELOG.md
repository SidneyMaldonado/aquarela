# CHANGELOG — AquarelaApi

Resumo das alterações realizadas:

- Scaffold do projeto criado com `dotnet new webapi` (target: .NET 10.0).
- Endpoint minimal `GET /weatherforecast` mantido do template inicial.
- Adicionado `Controllers/ExampleController.cs` com endpoints de exemplo:
  - `GET /api/example/hello` — retorna mensagem de saudação.
  - `GET /api/example/values` — retorna uma lista de valores.
- Habilitado OpenAPI/Swagger:
  - Mantido `Microsoft.AspNetCore.OpenApi` (API minimal).
  - Adicionado `Swashbuckle.AspNetCore` e configurado `AddSwaggerGen`, `UseSwagger` e `UseSwaggerUI`.
  - Swagger UI disponível em `/swagger/index.html` e OpenAPI JSON em `/swagger/v1/swagger.json`.
- Atualizado `Program.cs` para registrar controllers, MapControllers e expor Swagger UI.
- Adicionado `README.md` com instruções de execução e rotas de exemplo.

Como rodar localmente (exemplo):

```bash
cd AquarelaApi
dotnet restore
dotnet build
dotnet run --urls "http://localhost:5000"
# então acesse http://localhost:5000/swagger/index.html
```

Observações:
- Requer .NET 10 SDK instalado localmente.
- Commit criado contendo controller de exemplo e configuração do Swagger.

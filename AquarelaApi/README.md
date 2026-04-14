# AquarelaApi

Projeto ASP.NET Core Web API (target: .NET 10.0)

Como executar:

```bash
# na raiz do workspace
cd AquarelaApi
dotnet restore
dotnet build
dotnet run --urls "http://localhost:5000"
```

Endpoints:
- API padrão: `https://localhost:5001` e `http://localhost:5000`
- Exemplo de endpoint: `GET /weatherforecast`

Swagger / OpenAPI:
- OpenAPI JSON: `/openapi`
- Interface interativa: `/openapi/ui`

Exemplos de endpoints adicionados:
- `GET /api/example/hello` — retorna uma mensagem de saudação
- `GET /api/example/values` — retorna lista de valores de exemplo

Observações:
- Requer .NET 10 SDK instalado.
- Altere a porta em `--urls` se necessário.

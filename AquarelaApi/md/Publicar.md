# Publicar

## Publicação automática via GitHub Actions (recomendado)

O workflow `.github/workflows/deploy-azure.yml` realiza o deploy automaticamente a cada push na branch `main`.

### Configuração inicial (única vez)

1. Acesse o **Azure Portal** → **App Service** → `AquarelaConsignacao`
2. Vá em **Deployment Center** → **Manage publish profile** → **Download publish profile**
3. No GitHub, acesse **Settings → Secrets and variables → Actions → New repository secret**
   - Nome: `AZURE_WEBAPP_PUBLISH_PROFILE`
   - Valor: cole o conteúdo completo do arquivo `.PublishSettings` baixado
4. Faça push para a branch `main` — o deploy ocorrerá automaticamente

### Publicação manual (via terminal)

1. Navegue até a pasta do projeto
2. Execute: `dotnet publish AquarelaApi/AquarelaApi.csproj -c Release -o ./publish`
3. Faça upload da pasta `publish` via FTPS (credenciais no Azure Portal → Deployment Center)

### URL da aplicação

https://aquarelaconsignacao-fkdfb8eagzfwevfg.brazilsouth-01.azurewebsites.net/swagger



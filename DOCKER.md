# 🐳 Docker - Guia de Deploy da AquarelaApi

Este guia mostra como construir e executar a **AquarelaApi** usando Docker.

---

## 📋 Pré-requisitos

- **Docker Desktop** instalado ([download](https://www.docker.com/products/docker-desktop))
- **Docker Compose** (já incluído no Docker Desktop)

---

## 🚀 Opção 1: Docker Run (simples)

### 1. Build da imagem

```bash
docker build -t aquarelaapi:latest .
```

### 2. Executar o container

```bash
docker run -d \
  --name aquarela-api \
  -p 5000:8080 \
  -e "ConnectionStrings__DefaultConnection=Server=tcp:aquarela-sql.database.windows.net,1433;Initial Catalog=free-sql-db-5947062;User ID=aquarela-sql;Password=SUA_SENHA;Encrypt=True;" \
  -e "Jwt__Key=AquarelaSecretKey@2026!MustBe32Chars!" \
  -e "Jwt__Issuer=AquarelaApi" \
  -e "Jwt__Audience=AquarelaApi" \
  aquarelaapi:latest
```

### 3. Acessar a API

- Swagger: http://localhost:5000/swagger

### 4. Ver logs

```bash
docker logs -f aquarela-api
```

### 5. Parar e remover

```bash
docker stop aquarela-api
docker rm aquarela-api
```

---

## 🐙 Opção 2: Docker Compose (recomendado)

### 1. Configurar variáveis de ambiente

Copie `.env.example` para `.env` e preencha os valores reais:

```bash
cp .env.example .env
```

Edite o arquivo `.env`:

```env
CONNECTION_STRING=Server=tcp:aquarela-sql.database.windows.net,1433;Initial Catalog=free-sql-db-5947062;User ID=aquarela-sql;Password=SUA_SENHA_REAL;Encrypt=True;
JWT_KEY=SUA_CHAVE_SECRETA_COM_MINIMO_32_CARACTERES
```

### 2. Build e start

```bash
docker-compose up -d --build
```

### 3. Acessar a API

- Swagger: http://localhost:5000/swagger

### 4. Ver logs

```bash
docker-compose logs -f aquarelaapi
```

### 5. Parar

```bash
docker-compose down
```

### 6. Rebuild completo (limpar cache)

```bash
docker-compose down
docker-compose build --no-cache
docker-compose up -d
```

---

## 🔧 Comandos úteis

### Verificar containers rodando

```bash
docker ps
```

### Entrar no container (bash)

```bash
docker exec -it aquarela-api bash
```

### Ver uso de recursos

```bash
docker stats aquarela-api
```

### Remover imagens antigas

```bash
docker image prune -a
```

---

## 🌐 Deploy em produção

### Azure Container Registry (ACR)

```bash
# 1. Login no Azure
az login
az acr login --name seuregistry

# 2. Tag da imagem
docker tag aquarelaapi:latest seuregistry.azurecr.io/aquarelaapi:latest

# 3. Push
docker push seuregistry.azurecr.io/aquarelaapi:latest

# 4. Deploy no Azure App Service (Web App for Containers)
az webapp create \
  --resource-group seu-resource-group \
  --plan seu-app-service-plan \
  --name aquarela-api \
  --deployment-container-image-name seuregistry.azurecr.io/aquarelaapi:latest
```

### Docker Hub

```bash
# 1. Login
docker login

# 2. Tag
docker tag aquarelaapi:latest seuusuario/aquarelaapi:latest

# 3. Push
docker push seuusuario/aquarelaapi:latest
```

---

## 🔒 Segurança

⚠️ **NUNCA** commite:
- Arquivo `.env` com credenciais reais
- Connection strings com senhas
- JWT keys de produção

✅ Use:
- Azure Key Vault para secrets em produção
- GitHub Secrets para CI/CD
- Variáveis de ambiente do Azure App Service

---

## 📊 Monitoramento

### Health Check (adicione ao Dockerfile se necessário)

```dockerfile
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1
```

### Logs estruturados

A aplicação loga no stdout/stderr, que o Docker captura automaticamente.

---

## 🐛 Troubleshooting

### Container não inicia

```bash
docker logs aquarela-api
```

### Erro de connection string

Verifique as variáveis de ambiente:

```bash
docker exec aquarela-api printenv | grep Connection
```

### Porta já em uso

Mude a porta no `docker-compose.yml` ou `docker run -p 5001:8080 ...`

### Rebuild sem cache

```bash
docker build --no-cache -t aquarelaapi:latest .
```

---

## 📚 Mais informações

- [Documentação Docker](https://docs.docker.com/)
- [ASP.NET Core no Docker](https://learn.microsoft.com/aspnet/core/host-and-deploy/docker/)
- [Azure Container Instances](https://azure.microsoft.com/services/container-instances/)

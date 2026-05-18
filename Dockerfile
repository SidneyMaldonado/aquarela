# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar arquivos de projeto e restaurar dependências (cache layer)
COPY ["AquarelaApi/AquarelaApi.csproj", "AquarelaApi/"]
RUN dotnet restore "AquarelaApi/AquarelaApi.csproj"

# Copiar código-fonte e compilar
COPY . .
WORKDIR "/src/AquarelaApi"
RUN dotnet build "AquarelaApi.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "AquarelaApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Criar usuário não-root para segurança (sintaxe compatível com Debian/Alpine)
RUN useradd -m -s /bin/bash appuser && chown -R appuser:appuser /app
USER appuser

# Copiar artefatos publicados do stage anterior
COPY --from=publish /app/publish .

# Expor porta padrão do ASP.NET Core
EXPOSE 8080
EXPOSE 8081

# Variáveis de ambiente (podem ser sobrescritas no docker run ou docker-compose)
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Comando de inicialização
ENTRYPOINT ["dotnet", "AquarelaApi.dll"]

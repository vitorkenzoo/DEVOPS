# 1) Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 1.1) copia só o csproj e restaura (cache-friendly)
COPY ["FuracaoAlerta.API.csproj", "./"]
RUN dotnet restore "FuracaoAlerta.API.csproj"

# 1.2) copia todo o código e publica
COPY . .
RUN dotnet publish "FuracaoAlerta.API.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

# 2) Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# 2.1) força o Kestrel a escutar na porta 80
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# 2.2) copia só o que foi publicado
COPY --from=build /app/publish ./

# 2.3) entrypoint
ENTRYPOINT ["dotnet", "FuracaoAlerta.API.dll"]

# 1) Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5029

# 2) Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia csproj e restaura
COPY ["FuracaoAlerta.API.csproj", "./"]
RUN dotnet restore "./FuracaoAlerta.API.csproj"

# Copia todo o código e publica
COPY . .
RUN dotnet publish "FuracaoAlerta.API.csproj" -c Release -o /app/publish

# 3) Imagem final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Configurações de ambiente
ENV ASPNETCORE_URLS=http://+:5029
ENV ConnectionStrings__DefaultConnection="User Id=rm557245;Password=021005;Data Source=oracle.fiap.com.br:1521/ORCL;"

ENTRYPOINT ["dotnet", "FuracaoAlerta.API.dll"]

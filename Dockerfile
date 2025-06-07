# Etapa base com o ASP.NET Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Etapa de build com o SDK do .NET
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia apenas o .csproj e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia todos os arquivos restantes e compila
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Etapa final: junta tudo
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FuracaoAlerta.API.dll"]

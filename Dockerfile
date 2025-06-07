# — Etapa de build —
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app

# — Etapa de runtime —
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .

# Faz o Kestrel escutar em 0.0.0.0:8080
ENV ASPNETCORE_URLS="http://+:8080"

# Connection string para o Oracle no container oracle-db
ENV ConnectionStrings__DefaultConnection="User Id=rm557245;Password=021005;Data Source=oracle-db:1521/XE"


# Expõe a porta 8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "FuracaoAlerta.API.dll"]

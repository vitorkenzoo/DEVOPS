# — Etapa de build —
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish "FuracaoAlerta.API.csproj" -c Release -o /app

# — Etapa de runtime —
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .

# Kestrel em 0.0.0.0:8080
ENV ASPNETCORE_URLS="http://+:8080"

# Connection string para o Oracle (service XEPDB1)
ENV ConnectionStrings__DefaultConnection="User Id=system;Password=oracle;Data Source=oracle-db:1521/XEPDB1"

EXPOSE 8080

ENTRYPOINT ["dotnet", "FuracaoAlerta.API.dll"]

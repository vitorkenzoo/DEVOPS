# Runtime ASP.NET 9.0
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5029
ENV ASPNETCORE_URLS=http://+:5029

# Build com SDK 9.0
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["FuracaoAlerta.API.csproj", "./"]
RUN dotnet restore "FuracaoAlerta.API.csproj"
COPY . .
RUN dotnet build "FuracaoAlerta.API.csproj" -c Release -o /app/build

# Publicação
FROM build AS publish
RUN dotnet publish "FuracaoAlerta.API.csproj" -c Release -o /app/publish

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FuracaoAlerta.API.dll"]
# --- Etapa 1: imagem base de runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# --- Etapa 2: build e publish ---
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 1) Copia apenas o csproj e restaura dependências
COPY ["FuracaoAlerta.API.csproj", "./"]
RUN dotnet restore "FuracaoAlerta.API.csproj"

# 2) Copia todo o restante do código e faz build + publish
COPY . .
RUN dotnet publish "FuracaoAlerta.API.csproj" -c Release -o /app/publish

# --- Etapa 3: imagem final ---
FROM base AS final
WORKDIR /app

# Copia o resultado do publish
COPY --from=build /app/publish .

# Define o entrypoint
ENTRYPOINT ["dotnet", "FuracaoAlerta.API.dll"]

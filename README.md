# 🌪️ Furacão Alerta API

**Furacão Alerta** é uma API REST desenvolvida em ASP.NET Core (.NET 9) que oferece funcionalidades para monitoramento de furacões, gerenciamento de alertas, abrigos e usuários em situações de emergência.

---

## 📦 Tecnologias Utilizadas

- .NET 9 (ASP.NET Core Web API)
- Entity Framework Core (Oracle)
- Oracle Database 21c
- Swagger / OpenAPI 3.0
- Visual Studio Code
- VM linux
---

## 📁 Estrutura do Projeto

```
DEVOPS
├── Controllers
├── DTOs
├── Models
├── Services
├── Data
├── Program.cs
├── appsettings.json
├── FuracaoAlerta.API.csproj
├── Dockerfile
```

---

## 🔗 Endpoints Principais (via Swagger)

- `GET /api/alerta` – Listar alertas
- `GET /api/evento` – Listar eventos (furacões)
- `GET /api/usuario` – Listar usuários
- `GET /api/abrigo` – Listar abrigos
- `GET /api/endereco` – Listar endereços

CRUD completo disponível para todas as entidades.

---

## 🧪 Testes via Swagger

Acesse:

```
http://localhost:5029/swagger
```

> Você poderá testar todos os endpoints diretamente pela interface.

## ⚙️ Configuração

### 1. Configure sua string de conexão no `appsettings.json`:

```json
"ConnectionStrings": {
  "OracleConnection": "User Id=RM557245;Password=021005;Data Source=oracle.fiap.com.br:1521/ORCL;"
}
```

### 2. Comandos para rodar localmente:

```bash
dotnet build
dotnet ef database update
dotnet run
```

---

## 🧾 Documentação OpenAPI

- Swagger com metadados: título, descrição, contato e versão configurados
- Todas as entidades documentadas via Annotations + DTOs

---

## 👨‍💻 Desenvolvedores

- Vitor Kenzo Mizumoto
- Adriano Barutt

# Ambev Developer Evaluation

Este projeto foi desenvolvido como parte de um teste técnico para a Ambev. Trata-se de uma API de vendas construída com **.NET**, utilizando **DDD**, **CQRS**, **MediatR**, **FluentValidation**, **Entity Framework Core** e **PostgreSQL** (via Docker Compose).

---

## 📚 Sumário

- [Arquitetura do Projeto](#arquitetura-do-projeto)
- [Pré-requisitos](#pré-requisitos)
- [Configuração e Execução](#configuração-e-execução)
- [Testes](#testes)
- [Endpoints Principais](#endpoints-principais)
- [Contato](#contato)

---

## 🧱 Arquitetura do Projeto

A estrutura do projeto está organizada da seguinte forma:

```
Ambev.DeveloperEvaluation
├── Core
│   ├── Domain
│   └── Application
├── Crosscutting
│   ├── Ambev.DeveloperEvaluation.Common
│   └── Ambev.DeveloperEvaluation.IoC
├── Drivers
│   └── WebApi (Ambev.DeveloperEvaluation.WebApi)
└── Adapters
    └── Infrastructure (Ambev.DeveloperEvaluation.ORM)
```

- **Domain:** Regras de negócio, entidades e interfaces de repositório.
- **Application:** Casos de uso, comandos, queries e handlers via MediatR.
- **Infrastructure:** Implementação dos repositórios com EF Core e gerenciamento de migrações.
- **WebApi:** Exposição dos endpoints REST.

---

## ⚙️ Pré-requisitos

- **Docker**
- **.NET 8 SDK** ou superior
- **Git**

---

## 🚀 Configuração e Execução

```bash
# 1. Clone o repositório
git clone https://github.com/cassianomedrado/TesteTecnicoAmbevSalesApi.git
cd TesteTecnicoAmbevSalesApi

# 2. Suba os containers (PostgreSQL, etc.)
docker-compose up -d

# 3. Verifique se os containers estão rodando
docker-compose ps
```

### 🔧 Ajuste de Connection String (se necessário)

Abra `appsettings.Development.json` no projeto `Ambev.DeveloperEvaluation.WebApi`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=DevEval;Username=postgres;Password=postgres"
  }
}
```

> ⚠️ Ajuste conforme necessário com base no seu `docker-compose.yml`.

### 🛠️ Executar Migrations (opcional)

```bash
cd src/Ambev.DeveloperEvaluation.WebApi
dotnet ef database update
```

### ▶️ Executar a Aplicação

```bash
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
```

A API estará disponível em:  
👉 http://localhost:5000 ou https://localhost:5001

---

## ✅ Testes

Para rodar os testes:

```bash
cd tests/Ambev.DeveloperEvaluation.Tests
dotnet test
```

---

## 🔌 Endpoints Principais

Todos os endpoints seguem o padrão REST e estão sob o prefixo `/api/`. Exemplos:

| Método | Endpoint                     | Descrição              |
|--------|------------------------------|------------------------|
| POST   | `/api/sales`                 | Cria uma nova venda    |
| GET    | `/api/sales`                 | Lista todas as vendas  |
| GET    | `/api/sales/{id}`            | Detalha uma venda      |
| PUT    | `/api/sales/{id}/complete`   | Finaliza a venda       |
| PUT    | `/api/sales/{id}/cancel`     | Cancela a venda        |
| DELETE | `/api/sales/{id}`            | Remove uma venda       |
| ...    | CRUD completo de outras entidades como `Customers`, `Products`, etc. |

### 📖 Documentação via Swagger

Se habilitado, acesse:  
👉 http://localhost:5000/swagger ou http://localhost:5000/swagger/index.html

---

## 📬 Contato

Desenvolvido por **Cassiano Medrado** como parte do processo seletivo da Ambev.

- 🔗 [LinkedIn](https://www.linkedin.com/in/cassiano-medrado-2651781a4/)
- 🐙 [GitHub](https://github.com/cassianomedrado)

💬 Dúvidas sobre a configuração ou execução? Fique à vontade para abrir uma issue no repositório.

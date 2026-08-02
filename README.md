AegisAuth-README.md
# AegisAuth

**Multi-tenant Authentication & Authorization Platform**

A production-oriented .NET authentication system built with **Clean Architecture**.  
Supports secure user registration, JWT / OAuth-style tokens, refresh token management, role & scope based authorization, and includes a **Blazor Admin Portal**.

---

## Features

- **Multi-tenancy** — Full tenant isolation with domain-based tenant resolution
- **User Registration & Login**
- **JWT Access Tokens** + **Refresh Token** support
- **OAuth-style Token Endpoint**
- **Role & Scope based Authorization**
- **Client Management** (for machine-to-machine scenarios)
- **CQRS** with MediatR-style handlers + FluentValidation
- **Result Pattern** for clean error handling
- **Blazor Admin Portal** for tenant & user management
- **Docker** support
- **Scalar OpenAPI** documentation

---

## Architecture

```
src/
├── AegisAuth.API                 → ASP.NET Core Web API (Controllers, DI, Auth middleware)
├── AegisAuth.Application         → Use cases (Commands, Handlers, Validators, Behaviors)
├── AegisAuth.Domain              → Entities, Value Objects, Domain Errors, Shared Result
├── AegisAuth.Infrastructure      → Token service, Password hasher, external services
├── AegisAuth.Persistence         → EF Core DbContext, Configurations, Migrations
└── AegisAuth.AdminPortal         → Blazor Admin UI
```

---

## Tech Stack

| Layer              | Technology                          |
|--------------------|-------------------------------------|
| Framework          | .NET 9 / 10                         |
| Architecture       | Clean Architecture + CQRS           |
| API                | ASP.NET Core Minimal / Controllers  |
| Auth               | JWT Bearer + Cookie                 |
| Validation         | FluentValidation                    |
| ORM                | Entity Framework Core               |
| Admin UI           | Blazor                              |
| Documentation      | Scalar OpenAPI                      |
| Containerization   | Docker Compose                      |

---

## Getting Started

### Prerequisites

- .NET 9 or .NET 10 SDK
- Docker (optional but recommended)
- PostgreSQL or SQL Server

### 1. Clone the repository

```bash
git clone https://github.com/onurkarakus/AegisAuth.git
cd AegisAuth
```

### 2. Run with Docker (Recommended)

```bash
cd docker
docker compose up -d --build
```

### 3. Run locally

```bash
# Apply migrations (if needed)
dotnet ef database update --project src/AegisAuth.Persistence --startup-project src/AegisAuth.API

# Run the API
dotnet run --project src/AegisAuth.API
```

API will be available at:  
`https://localhost:5xxx`  
Scalar documentation: `/scalar/v1`

---

## Core Endpoints (Example)

| Method | Endpoint                  | Description                  |
|--------|---------------------------|------------------------------|
| POST   | `/api/auth/register`      | Register a new user          |
| POST   | `/api/auth/login`         | Login and receive tokens     |
| POST   | `/api/oauth/token`        | Generate access + refresh token |
| GET    | `/api/diagnostics`        | Health / diagnostics         |

> Detailed API documentation is available via Scalar when running in Development mode.

---

## Project Structure Highlights

- **Domain** → Rich domain model (`Tenant`, `User`, `Client`, `Role`, `Scope`, `RefreshToken`)
- **Application** → Feature-based folders (`Features/Auth/Login`, `Features/Auth/Register`, `Features/OAuth/Token`)
- **Persistence** → Fully configured EF Core with Fluent API configurations + migrations
- **API** → JWT + Cookie authentication, policy-based authorization

---

## Roadmap

- [x] Multi-tenant core
- [x] User registration & login
- [x] JWT + Refresh Token
- [x] Role & Scope support
- [x] Blazor Admin Portal (basic)
- [ ] Full admin CRUD operations
- [ ] Email confirmation & password reset
- [ ] Audit logging
- [ ] Rate limiting & security hardening
- [ ] Comprehensive integration tests

---

## License

This project is licensed under the **MIT License**.

---

<div align="center">
  <sub>Built with Clean Architecture principles and a focus on multi-tenant security.</sub>
</div>

```markdown
# Talabat API

![.NET](https://img.shields.io/badge/.NET%208-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![Clean Architecture](https://img.shields.io/badge/Clean%20Architecture-Enterprise-blue?style=for-the-badge)

Production-grade **ASP.NET Core Web API** built with **.NET 8**  
following **Clean Architecture** + **Domain-Driven Design (DDD)** principles.

Showcases modern, scalable, testable enterprise backend patterns.

---

## Project Overview

Complete e-commerce backend featuring:

- Product catalog (filtering, pagination, brands & types)
- Shopping basket & checkout flow
- Order creation & lifecycle
- Secure authentication & authorization (JWT + Identity)
- Stripe payments (Payment Intents + Webhooks)
- Clean separation of concerns

Structured as **4 independent projects** using **Clean Architecture**.

---

## Architecture

```
Talabat.sln
├─ Talabat.APIs          Presentation     (Controllers, DTOs, Middleware, Swagger)
├─ Talabat.Core          Domain           (Entities, Aggregates, Interfaces, Specs)
├─ Talabat.Repository    Infrastructure   (EF Core, Repositories, Data Seed)
└─ Talabat.Service       Application      (Services, Use Cases, Orchestration)
```

**Dependency direction**  
→ Outer layers depend **only** inward  
→ `Core` depends on **nothing**

---

## Layers Breakdown

### 1. Talabat.APIs – Presentation

**Responsibilities**  
HTTP • Validation • Mapping • Auth • Exceptions • Swagger

**Main folders**  
- Controllers/ (Accounts, Products, Baskets, Orders, Payments)  
- Dtos/  
- Middlewares/  
- Extensions/  
- Helpers/ (AutoMapper, resolvers)

**Patterns**  
DTO • AutoMapper • Middleware

### 2. Talabat.Core – Domain (Pure Business)

**Responsibilities**  
Entities • Aggregates • Business rules • Specifications

**Main folders**  
- Entities/  
- Order_Aggregation/  
- Interfaces/ (IGenericRepository, IBasketRepository, IUnitOfWork…)  
- Specifications/  
- UnitOfWork.cs

**Patterns**  
DDD • Specification • Aggregate Root  
**No infrastructure** (no EF, no Stripe, no web)

### 3. Talabat.Repository – Infrastructure

**Responsibilities**  
EF Core • Repositories • Migrations • Data seeding • Identity store

**Main folders**  
- Data/ (StoreContext)  
- Configurations/  
- Migrations/  
- DataSeed/  
- Repositories/

**Patterns**  
Repository • Unit of Work • Fluent API

### 4. Talabat.Service – Application

**Responsibilities**  
Orchestration • Payment logic • Token generation • Order workflow

**Main services**  
- OrderService  
- PaymentService  
- TokenService

**Patterns**  
Application Services • Facade

---

## Stripe Integration

- PaymentIntent creation  
- Webhook processing + signature verification  
- Order status sync (`Pending` → `PaymentReceived` / `Failed`)

**Events**  
`payment_intent.succeeded`  
`payment_intent.payment_failed`

**Endpoint**  
`POST /api/payments/webhook`

> Webhook secret **never** committed — use user-secrets or env vars.

---

## Security

- ASP.NET Core Identity  
- JWT Bearer tokens  
- Role & policy authorization  
- PBKDF2 hashing  
- Short-lived tokens

---

## Configuration

| File                           | Purpose                              |
|--------------------------------|--------------------------------------|
| `appsettings.json`             | Base / safe defaults                 |
| `appsettings.Development.json` | Local secrets & connection strings   |
| `appsettings.Example.json`     | Template for contributors            |

**Never commit**: JWT key, Stripe keys, webhook secret, DB credentials

---

## Quick Start

```bash
# Clone
git clone https://github.com/AhmedFarouk04/TalabatApi.git
cd TalabatApi

# Config
cp appsettings.Example.json appsettings.Development.json
# or use user-secrets:
dotnet user-secrets init
dotnet user-secrets set "Jwt:Key"        "your-very-long-secret-key"
dotnet user-secrets set "Stripe:SecretKey" "sk_test_..."

# Database + seed
dotnet ef database update --project Talabat.Repository --startup-project Talabat.APIs

# Run
dotnet run --project Talabat.APIs
```

→ Swagger: `https://localhost:<port>/swagger`

---

## Design Patterns & Principles

- Clean Architecture  
- Domain-Driven Design  
- Specification Pattern  
- Repository + Unit of Work  
- Dependency Injection  
- DTO + AutoMapper  
- CQRS-lite separation  
- Middleware pipeline

**Benefits**  
→ Testable  
→ Maintainable  
→ Scalable  
→ Enterprise-grade boundaries

---

## License

MIT License  
(Feel free to use for learning / portfolio – attribution appreciated)

Happy coding! 🚀

جرب تحطه واعمل refresh للصفحة — المفروض يبقى **أنظف وأجمل** بكتير في الـ dark theme.  
لو عايز نعدل حاجة تانية (نضيف Tests، Docker، CI، API examples …) قولي.

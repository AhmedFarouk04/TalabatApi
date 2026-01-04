```markdown
# Talabat API

![.NET 8](https://img.shields.io/badge/.NET%208-5C2D91?style=flat&logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/c%23-239120?style=flat&logo=c-sharp&logoColor=white)
![Clean Architecture](https://img.shields.io/badge/Clean%20Architecture-Enterprise-blue?style=flat)

Production-ready e-commerce backend  
built with **ASP.NET Core 8** • **Clean Architecture** • **DDD**

---

## Features

- Product catalog (filter, paginate, brands & types)
- Shopping basket
- Order creation & management
- Customer authentication (JWT + Identity)
- Stripe Payment Intents + Webhook
- Clean layered architecture

---

## Structure

```
Talabat.sln
├── Talabat.APIs        Presentation
├── Talabat.Core        Domain
├── Talabat.Repository  Infrastructure
└── Talabat.Service     Application
```

**Dependency flow** → inward only  
`Core` ← has **zero** dependencies

---

## Layers at a glance

**Talabat.APIs**  
Controllers • DTOs • Auth • Swagger • Exception handling

**Talabat.Core**  
Entities • Aggregates • Specifications • Interfaces  
No EF / no external deps

**Talabat.Repository**  
EF Core • DbContext • Repositories • Migrations • Seed

**Talabat.Service**  
OrderService • PaymentService • TokenService  
Business orchestration

---

## Stripe

- Payment Intent creation
- Webhook (`/api/payments/webhook`)
- Events: `succeeded`, `payment_failed`
- Signature verification
- Order status update

> Secret **never** in git — use user-secrets / env

---

## Security

- ASP.NET Identity
- JWT Bearer
- PBKDF2 hashing
- Role-based authorization

---

## Configuration

```text
appsettings.json           → base config
appsettings.Development.json → secrets (git ignored)
appsettings.Example.json   → template
```

Never commit: JWT key, Stripe keys, webhook secret

---

## Quick Start

```bash
git clone https://github.com/AhmedFarouk04/TalabatApi.git
cd TalabatApi

# Config (choose one)
cp appsettings.Example.json appsettings.Development.json
# or
dotnet user-secrets set "Jwt:Key" "your-very-long-key"
dotnet user-secrets set "Stripe:SecretKey" "sk_test_..."

# Database
dotnet ef database update --project Talabat.Repository --startup-project Talabat.APIs

# Run
dotnet run --project Talabat.APIs
```

Swagger → `/swagger`

---

## Main Patterns

- Clean Architecture
- DDD
- Specification Pattern
- Repository + Unit of Work
- Dependency Injection
- DTO + AutoMapper

**Why?** Testable • Maintainable • Scalable

---

## License

MIT — for learning & portfolio use

Happy coding! 🚀

لو لسه فيه جزء معين مش عاجبك (الـ tree، الـ badges، الـ quick start، أي حاجة)، قولي بالظبط إيه اللي مضايقك ونعدله مع بعض لحد ما يبقى مريح 100%.

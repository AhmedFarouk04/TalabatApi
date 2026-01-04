```markdown
# Talabat API

Production-ready e-commerce backend  
built with ASP.NET Core 8 • Clean Architecture • DDD

---

## Features

- Product catalog (filter, paginate, brands & types)  
- Shopping basket  
- Order creation & management  
- Customer authentication (JWT + Identity)  
- Stripe Payment Intents + Webhook  
- Clean layered architecture

---

## Project Structure

```
Talabat.sln
├── Talabat.APIs         Presentation Layer
├── Talabat.Core         Domain Layer
├── Talabat.Repository   Infrastructure Layer
└── Talabat.Service      Application Layer
```

Core has zero dependencies  
All outer layers depend inward only

---

## Layers Summary

**Talabat.APIs**  
Controllers, DTOs, Auth, Swagger, Error handling

**Talabat.Core**  
Entities, Aggregates, Specifications, Interfaces  
(No EF, no external code)

**Talabat.Repository**  
EF Core, DbContext, Repositories, Migrations, Data Seed

**Talabat.Service**  
OrderService, PaymentService, TokenService  
Business logic orchestration

---

## Stripe Payment

- Create Payment Intent  
- Webhook endpoint: `/api/payments/webhook`  
- Events: succeeded, payment_failed  
- Signature verification  
- Update order status automatically  

Webhook secret stored in user-secrets or environment (never in git)

---

## Security

- ASP.NET Identity  
- JWT Bearer tokens  
- Secure password hashing  
- Role-based authorization  

---

## Configuration

Use one of these files:

- `appsettings.json` → basic config  
- `appsettings.Development.json` → your secrets (git ignored)  
- `appsettings.Example.json` → template to copy  

Never commit: JWT key, Stripe keys, webhook secret

---

## How to Run

```bash
git clone https://github.com/AhmedFarouk04/TalabatApi.git
cd TalabatApi

# Add your secrets (choose one way)
# Way 1: copy example
cp appsettings.Example.json appsettings.Development.json

# Way 2: use user-secrets
dotnet user-secrets set "Jwt:Key" "your-very-long-key"
dotnet user-secrets set "Stripe:SecretKey" "sk_test_..."

# Create & seed database
dotnet ef database update --project Talabat.Repository --startup-project Talabat.APIs

# Start the API
dotnet run --project Talabat.APIs
```

Open Swagger: https://localhost:<port>/swagger

---

## Main Patterns Used

- Clean Architecture  
- Domain-Driven Design  
- Specification Pattern  
- Repository + Unit of Work  
- DTO + AutoMapper  

→ Testable, maintainable, scalable

---

MIT License  
For learning and portfolio use  
Happy coding! 🚀
```


```markdown
# Talabat API

A production-grade **ASP.NET Core Web API** built with **.NET 8**, following **Clean Architecture** and **Domain-Driven Design (DDD)** principles.  
The project showcases modern best practices for building scalable, maintainable, testable, and enterprise-ready backend systems.

---

## 📌 Project Overview

**Talabat API** is a complete e-commerce backend that handles:

- Product catalog with advanced filtering & pagination
- Shopping basket & checkout process
- Orders creation & lifecycle management
- Secure customer authentication & authorization
- Stripe payment integration (Payment Intents + Webhooks)
- Clean separation of concerns across all layers

The solution is structured into **4 independent projects** using **Clean Architecture**.

---

## 🏗 Architecture Overview

```
Talabat.API.sln
│
├── Talabat.APIs          → Presentation Layer (Controllers + DTOs + Middleware)
├── Talabat.Core          → Domain Layer        (Entities + Interfaces + Business Rules)
├── Talabat.Repository    → Infrastructure      (EF Core + Repositories + Data Seed)
└── Talabat.Service       → Application Layer   (Services + Orchestration + Use Cases)
```

### Dependency Rule (Clean Architecture)
- Outer layers depend **only** on inner layers
- `Core` depends on **nothing**
- `Repository` & `Service` depend on `Core`
- `APIs` depends on `Core`, `Service` & `Repository`

---

## 📦 Layers Breakdown

### 1️⃣ Talabat.APIs (Presentation Layer)

**Responsibilities**  
- HTTP endpoints & routing  
- Input validation & DTO → Entity mapping  
- Authentication / Authorization / Identity  
- Global exception handling  
- Swagger OpenAPI documentation  

**Key folders & files**  
- `Controllers/`  
  - AccountsController  
  - ProductsController  
  - BasketsController  
  - OrdersController  
  - PaymentsController  
- `Dtos/`  
- `Middlewares/` → Global error handling  
- `Extensions/` → Service collection helpers  
- `Helper/` → AutoMapper profiles, resolvers  
- `Program.cs` & `appsettings*.json`

**Patterns**  
DTO • AutoMapper • Middleware • Minimal APIs style (optional)

---

### 2️⃣ Talabat.Core (Domain Layer)

**The heart of the application — pure business logic**

**Responsibilities**  
- Domain entities & value objects  
- Aggregates & business invariants  
- Repository & service interfaces  
- Query specifications  

**Key components**  
- `Entities/` → Product, Basket, Order, OrderItem, ...  
- `Order_Aggregation/` → Order + Address + DeliveryMethod  
- `Interfaces/` → IGenericRepository, IBasketRepository, IUnitOfWork, ...  
- `Specifications/` → ProductWithBrandAndTypeSpec, OrderByPaymentIntentSpec, ...  
- `UnitOfWork.cs`

**Patterns**  
DDD • Specification Pattern • Repository Interface • Aggregate Root

🚫 **Zero infrastructure here** (no EF, no Stripe, no HTTP)

---

### 3️⃣ Talabat.Repository (Infrastructure Layer)

**Responsibilities**  
- EF Core DbContext & configurations  
- Concrete repository implementations  
- Data seeding (brands, types, products, delivery methods)  
- Identity persistence (users & roles)

**Key components**  
- `Data/StoreContext.cs`  
- `Configurations/` → Fluent API entity mappings  
- `Migrations/`  
- `DataSeed/`  
- `Repositories/` → Generic + Basket-specific  
- `UnitOfWork.cs`

**Patterns**  
Repository Pattern • Unit of Work • EF Core Fluent API

---

### 4️⃣ Talabat.Service (Application Layer)

**Responsibilities**  
- Orchestrate business use cases  
- Coordinate domain logic & infrastructure  
- Handle payment flows & token generation  
- Implement complex order workflows

**Key services**  
- `OrderService`  
- `PaymentService`  
- `TokenService`

**Patterns**  
Application Services • Use Case orchestration • Facade

---

## 💳 Stripe Payment Integration

- Payment Intent creation & client-secret return  
- Webhook endpoint to listen for Stripe events  
- Signature verification for security  
- Automatic order status update (`Pending → PaymentReceived / Failed`)

**Supported webhook events**  
- `payment_intent.succeeded`  
- `payment_intent.payment_failed`

**Endpoint**  
`POST /api/payments/webhook`

> **Important**: Webhook secret is stored in user-secrets / environment variables — **never** committed.

---

## 🔐 Authentication & Security

- **ASP.NET Core Identity**  
- **JWT Bearer** authentication  
- Role-based & policy-based authorization  
- Secure password hashing (PBKDF2)  
- Token revocation not implemented (short-lived tokens)

---

## ⚙ Configuration & Secrets Management

| File                            | Purpose                              |
|--------------------------------|--------------------------------------|
| `appsettings.json`             | Base / safe defaults                 |
| `appsettings.Development.json` | Local secrets & connection strings   |
| `appsettings.Example.json`     | Template for contributors            |

**Never commit**: JWT key, Stripe secret key, webhook signing secret, database passwords

---

## 🚀 Quick Start (Local Development)

```bash
# 1. Clone & enter directory
git clone https://github.com/your-username/TalabatApi.git
cd TalabatApi

# 2. Copy example config & fill your secrets
cp appsettings.Example.json appsettings.Development.json

# 3. (optional) Use user-secrets for sensitive values
dotnet user-secrets set "Jwt:Key" "your-very-long-secret-key"
dotnet user-secrets set "Stripe:SecretKey" "sk_test_..."

# 4. Apply migrations & seed data
dotnet ef database update --project Talabat.Repository --startup-project Talabat.APIs

# 5. Run the API
dotnet run --project Talabat.APIs
```

Swagger UI usually available at: `https://localhost:5001/swagger`

---

## 🛠 Design Patterns & Principles Used

- Clean Architecture  
- Domain-Driven Design (DDD)  
- Specification Pattern  
- Repository + Unit of Work  
- Dependency Injection  
- DTO + AutoMapper  
- CQRS-lite style separation  
- Middleware pipeline  
- Vertical Slice hints (future-ready)

**Why this structure?**  
Testability • Maintainability • Scalability • Clear boundaries • Real enterprise patterns

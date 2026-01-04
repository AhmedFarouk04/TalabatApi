تمام 👌
ده **README.md معدل ومحسّن بالكامل**، مرتب، احترافي، وجاهز كوبي-باست **زي ما هو** على GitHub بدون أي مشاكل تنسيق أو بلوكات مكسورة:

```markdown
# Talabat API

🚀 **Production-ready e-commerce backend**  
Built with **ASP.NET Core 8** • **Clean Architecture** • **Domain-Driven Design (DDD)**

---

## ✨ Features

- 🛒 Product catalog (filtering, pagination, brands & types)
- 🧺 Shopping basket (Redis)
- 📦 Order creation & management
- 👤 Customer authentication (JWT + ASP.NET Identity)
- 💳 Stripe Payment Intents + Webhook integration
- 🧱 Clean layered architecture with strict dependency rules
- 🧪 Ready for testing & extension

---

## 🏗️ Project Structure

```

Talabat.sln
├── Talabat.APIs         → Presentation Layer
├── Talabat.Core         → Domain Layer
├── Talabat.Repository   → Infrastructure Layer
└── Talabat.Service      → Application Layer

```

### Dependency Rule
- **Talabat.Core** has **zero external dependencies**
- All outer layers depend **inward only**
- No circular references

---

## 🧩 Layers Overview

### 🔹 Talabat.APIs (Presentation Layer)
- Controllers
- DTOs
- Authentication & Authorization
- Swagger
- Global error handling
- Middleware & Extensions

### 🔹 Talabat.Core (Domain Layer)
- Entities & Value Objects
- Aggregates (Order Aggregate)
- Specifications
- Interfaces (Repositories, Services)
- Domain enums & base classes  
❌ No EF Core  
❌ No external libraries

### 🔹 Talabat.Repository (Infrastructure Layer)
- Entity Framework Core
- DbContexts
- Repository implementations
- Unit of Work
- Migrations
- Database seeding

### 🔹 Talabat.Service (Application Layer)
- Business logic orchestration
- OrderService
- PaymentService
- TokenService
- Coordinates domain & infrastructure

---

## 💳 Stripe Payment Flow

- Create **Payment Intent**
- Webhook endpoint:  
```

POST /api/payments/webhook

````
- Handled events:
- `payment_intent.succeeded`
- `payment_intent.payment_failed`
- Signature verification enabled
- Order status updated automatically

🔐 **Important:**  
Stripe keys & webhook secret are stored in:
- `appsettings.Development.json`
- User Secrets
- Environment variables  
❌ Never committed to Git

---

## 🔐 Security

- ASP.NET Core Identity
- JWT Bearer Authentication
- Secure password hashing
- Role-based authorization
- Claims-based user access

---

## ⚙️ Configuration Strategy

The project uses **environment-based configuration**:

- `appsettings.json`  
→ Base (safe, no secrets)

- `appsettings.Development.json`  
→ Local secrets (❌ git ignored)

- `appsettings.Example.json`  
→ Template for developers

### ❌ Never commit:
- JWT Secret Key
- Stripe Secret / Publishable Keys
- Stripe Webhook Secret
- Production connection strings

---

## ▶️ How to Run Locally

```bash
git clone https://github.com/AhmedFarouk04/TalabatApi.git
cd TalabatApi
````

### 1️⃣ Configuration (choose one)

**Option A — Copy example**

```bash
cp appsettings.Example.json appsettings.Development.json
```

**Option B — User Secrets**

```bash
dotnet user-secrets set "JWT:Key" "your-super-secret-key"
dotnet user-secrets set "StripeSettings:SecretKey" "sk_test_..."
dotnet user-secrets set "StripeSettings:WebhookSecret" "whsec_..."
```

### 2️⃣ Database setup

```bash
dotnet ef database update \
  --project Talabat.Repository \
  --startup-project Talabat.APIs
```

### 3️⃣ Run the API

```bash
dotnet run --project Talabat.APIs
```

📄 Swagger UI:

```
https://localhost:<port>/swagger
```

---

## 🧠 Design Patterns & Practices

* Clean Architecture
* Domain-Driven Design (DDD)
* Specification Pattern
* Repository Pattern
* Unit of Work
* DTO + AutoMapper
* Dependency Injection
* SOLID Principles

✅ Testable
✅ Maintainable
✅ Scalable

---

## 👨‍💻 Author

**Ahmed Farouk**
Backend .NET Developer
Clean Architecture • APIs • Payments • Identity

---

⭐ If you find this project useful, feel free to star the repository!

```

- 

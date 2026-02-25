# MooMoo API - .NET Core 8

Clean Architecture implementation for MooMoo project using .NET Core 8.

## 🏗️ Architecture

```
MooMoo.Domain/          # Entities, Enums, Domain logic
MooMoo.Application/     # Use Cases, DTOs, Interfaces
MooMoo.Infrastructure/  # EF Core, DbContext, External services
MooMoo.Api/            # Controllers, REST API endpoints
```

## 🚀 Quick Start

### Option 1: Docker Compose (Recommended)

```bash
# Build and run (API + PostgreSQL)
docker-compose up -d

# View logs
docker-compose logs -f api

# Stop services
docker-compose down
```

API will be available at: `http://localhost:5000`

### Option 2: Local Development

**Prerequisites:**
- .NET 8 SDK
- PostgreSQL 16 (or use Docker for DB only)

**Run PostgreSQL only:**
```bash
docker-compose up -d db
```

**Run API locally:**
```bash
cd MooMoo.Api
dotnet restore
dotnet ef database update --project ../MooMoo.Infrastructure
dotnet run
```

## 📦 Database Migrations

```bash
# Add new migration
dotnet ef migrations add MigrationName --project MooMoo.Infrastructure --startup-project MooMoo.Api

# Update database
dotnet ef database update --project MooMoo.Infrastructure --startup-project MooMoo.Api

# Remove last migration
dotnet ef migrations remove --project MooMoo.Infrastructure --startup-project MooMoo.Api
```

## 🔌 Endpoints

### US01: Parent Registration

**POST** `/api/auth/register`

**Request:**
```json
{
  "email": "parent@example.com",
  "password": "password123",
  "name": "John Doe"
}
```

**Response (201 Created):**
```json
{
  "user": {
    "id": 1,
    "email": "parent@example.com",
    "role": "PARENT"
  },
  "message": "Account created successfully. You can now login."
}
```

## 🛠️ Tech Stack

- **.NET 8.0** - Framework
- **ASP.NET Core** - Web API
- **Entity Framework Core 8** - ORM
- **PostgreSQL 16** - Database
- **BCrypt.Net** - Password hashing
- **Swagger** - API documentation

## 📊 Database Schema

- **User** - Parent/Child accounts with OAuth support
- **Profile** - User profiles with grass/gold resources

## 🔍 Swagger UI

Visit `http://localhost:5000/swagger` when running in Development mode.

## 🆚 Comparison with NestJS

| Feature | NestJS (../api) | .NET Core (api_v2) |
|---------|----------------|-------------------|
| Language | TypeScript | C# |
| Port | 3000 | 5000 |
| PostgreSQL Port | 5432 | 5433 |
| ORM | Prisma | EF Core |
| Validation | class-validator | Data Annotations |
| Architecture | Clean Architecture | Clean Architecture |

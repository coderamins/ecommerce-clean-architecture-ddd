# Ecommerce — Clean Architecture + DDD + CQRS

Production-oriented ASP.NET Core Web API built using **Clean Architecture**, **Domain-Driven Design (DDD)**, **CQRS**, and **Event-Driven Architecture** principles.

The goal of this project is to demonstrate how modern enterprise applications can be designed with maintainability, scalability, and separation of concerns in mind.

---

# Tech Stack

* ASP.NET Core (.NET 9)
* Entity Framework Core
* PostgreSQL
* MediatR
* FluentValidation
* Docker
* Clean Architecture
* Domain-Driven Design (DDD)

---

# Project Structure

```text
src
├── Ecommerce.Api
├── Ecommerce.Application
├── Ecommerce.Domain
└── Ecommerce.Infrastructure
```

---

# Architecture

The project follows **Clean Architecture**.

```text
API
│
▼
Application
│
▼
Domain
│
▼
Infrastructure
```

## Layers

### Domain

Contains business rules and domain logic.

* Aggregate Roots
* Entities
* Value Objects
* Domain Events
* Business Rules

---

### Application

Implements application use cases.

* CQRS
* MediatR
* Vertical Slice Architecture
* Pipeline Behaviors
* FluentValidation
* Commands
* Queries

---

### Infrastructure

Contains technical implementations.

* Entity Framework Core
* PostgreSQL
* Repositories
* Outbox Pattern
* Event Dispatcher
* Event Registry
* Projections
* Read Models

---

### API

Application entry point.

* REST API
* Controllers
* Dependency Injection
* Swagger

---

# Architectural Patterns

* ✅ Clean Architecture
* ✅ Domain-Driven Design (DDD)
* ✅ CQRS
* ✅ Vertical Slice Architecture
* ✅ Repository Pattern
* ✅ Unit of Work
* ✅ Transaction Manager
* ✅ Domain Events
* ✅ Outbox Pattern
* ✅ Event Dispatcher
* ✅ Event Registry
* ✅ Projection Pattern
* ✅ Read Model Pattern
* ✅ Idempotent Consumer
* ✅ Dependency Injection

---

# Request Flow

## Command

```text
Client
    │
    ▼
Controller
    │
    ▼
MediatR
    │
    ▼
Logging Behavior
    │
    ▼
Validation Behavior
    │
    ▼
Transaction Behavior
    │
    ▼
Command Handler
    │
    ▼
Repository
    │
    ▼
Aggregate
    │
    ▼
Outbox
```

---

## Event Processing

```text
Outbox

↓

Outbox Processor

↓

Event Dispatcher

↓

Projection

↓

Read Model
```

---

## Query

```text
Client

↓

Controller

↓

MediatR

↓

Query Handler

↓

Read Repository

↓

Read Model
```

---

# Implemented Features

## Domain

* ✅ Rich Domain Model
* ✅ Aggregate Root
* ✅ Value Objects
* ✅ Domain Events

## Application

* ✅ CQRS
* ✅ MediatR
* ✅ Vertical Slice Architecture
* ✅ FluentValidation
* ✅ Validation Pipeline
* ✅ Logging Pipeline
* ✅ Transaction Pipeline

## Infrastructure

* ✅ Entity Framework Core
* ✅ PostgreSQL
* ✅ Repository Pattern
* ✅ Unit of Work
* ✅ Transaction Manager
* ✅ Outbox Pattern
* ✅ Event Dispatcher
* ✅ Event Registry
* ✅ Event Projections
* ✅ Read Models
* ✅ Idempotent Event Processing

---

# Roadmap

## Messaging

* [ ] RabbitMQ Integration
* [ ] Inbox Pattern
* [ ] Distributed Event Bus

## Distributed Systems

* [ ] Saga Pattern
* [ ] Distributed Transactions

## Performance

* [ ] Redis Cache
* [ ] Query Caching
* [ ] Response Caching

## Observability

* [ ] Serilog
* [ ] Structured Logging
* [ ] OpenTelemetry
* [ ] Metrics
* [ ] Health Checks

## Testing

* [ ] Unit Tests
* [ ] Integration Tests
* [ ] Architecture Tests

## Deployment

* [x] Docker
* [ ] Docker Compose (Production)
* [ ] Kubernetes
* [ ] CI/CD Pipeline

---

# Run

```bash
docker compose up -d

dotnet ef database update

dotnet run --project src/Ecommerce.Api
```

---

# Swagger

```
http://localhost:5000/swagger
```

---

# Project Status

🚧 **Actively under development**

This project is continuously evolving toward a production-ready, enterprise-grade architecture by incrementally implementing advanced architectural patterns and best practices.

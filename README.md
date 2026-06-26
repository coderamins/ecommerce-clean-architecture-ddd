# \# Ecommerce Clean Architecture + DDD

# 

# Production-oriented ASP.NET Core Web API built using Clean Architecture and Domain-Driven Design (DDD).

# 

# \---

# 

# \## Tech Stack

# 

# \* ASP.NET Core (.NET 9)

# \* Entity Framework Core

# \* PostgreSQL

# \* Docker

# \* Clean Architecture

# \* Domain-Driven Design (DDD)

# 

# \---

# 

# \## Project Structure

# 

# ```plaintext

# src

# ├── Ecommerce.Api

# ├── Ecommerce.Application

# ├── Ecommerce.Domain

# └── Ecommerce.Infrastructure

# ```

# 

# \---

# 

# \## Architecture

# 

# Layers:

# 

# \* Domain

# \* Application

# \* Infrastructure

# \* API

# 

# Principles:

# 

# \* Dependency Inversion

# \* Persistence Ignorance

# \* Rich Domain Model

# \* Aggregate Root

# \* Repository Pattern

# 

# \---

# 

# \## Current Features

# 

# \* Order Aggregate

# \* Create Order Use Case

# \* Repository Pattern

# \* PostgreSQL Persistence

# 

# \---

# 

# \## Planned Features

# 

# \* CQRS

# \* FluentValidation

# \* Result Pattern

# \* Global Exception Handling

# \* Domain Events

# \* Unit Tests

# \* Integration Tests

# \* Docker Environment

# \* CI/CD

# 

# \---

# 

# \## Run

# 

# ```bash

# docker compose up -d

# 

# dotnet ef database update

# 

# dotnet run --project Ecommerce.Api

# ```

# 

# Swagger:

# 

# ```plaintext

# http://localhost:5000/swagger

# ```




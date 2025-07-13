# Product Management Solution

A modular .NET 9 solution for managing products, following clean architecture principles.

## Projects

- **ProductManagement.API**: ASP.NET Core Web API for product management. Handles HTTP requests, validation, logging, and error handling.
- **ProductManager.Application**: Business logic and service interfaces/implementations.
- **ProductManagement.Infrastructure**: Data access layer, repositories, and EF Core context.
- **ProductManagement.Domain**: Core domain entities (e.g., Product).
- **ProductManager.Test**: Unit and integration tests for business logic, domain, and repository.

## Features

- Clean separation of concerns (API, Application, Infrastructure, Domain)
- Dependency Injection throughout
- DTOs for API requests/responses
- FluentValidation for model validation
- Serilog for structured logging
- Global exception handling middleware
- OpenAPI/Swagger for API documentation
- CORS enabled for development
- Comprehensive unit and integration tests

## Getting Started

1. **Build the Solution**dotnet build2. **Run the API**dotnet run --project ProductManagement.API3. **Run Tests**dotnet test ProductManager.Test4. **API Documentation**
   - Swagger UI available at `/swagger` when running the API.

## Project Structure
ProductManagementSolution/
├── ProductManagement.API/           # Web API
├── ProductManager.Application/      # Application/business logic
├── ProductManagement.Infrastructure/# Data access/repositories
├── ProductManagement.Domain/        # Domain entities
├── ProductManager.Test/             # Unit/integration tests
```

# Books APIя Project Brief

  ## Project Overview

  This is a .NET-based Books API template project implementing a clean
  architecture pattern for book management. The project serves as a
  foundation for building book-related applications with RESTful API
  endpoints.

  ## Architecture & Structure

  Clean Architecture Implementation

  - Books.API: Web API layer with controllers and HTTP endpoints
  - Books.Domain: Business logic, models, services, and interfaces
  - Books.Data: Data access layer with DAOs and entities
  - Books.Common: Shared utilities and cross-cutting concerns
  - Books.UnitTests: Unit testing project

  Key Design Patterns

  - Dependency Injection: Modular service registration via extension methods
  - Repository Pattern: Data access abstraction through IBookDao interface
  - Result Pattern: Custom TryResult implementation for error handling
  - Service Layer: Business logic encapsulation in dedicated service classes
  - Mapper Pattern: DTO-to-domain model conversion

  ## Technical Implementation

  Error Handling Approach

  - Custom TryResult<T> monad-style pattern for functional error handling
  - Structured error codes via dedicated ErrorCodes classes
  - Centralized error response handling in base controller
  - Implicit conversion operators for seamless error propagation

  Layered Architecture

  - Controllers delegate to domain services
  - Services coordinate business logic and data access
  - DAOs handle data persistence concerns
  - Mappers transform between API contracts and domain models

  Dependency Management

  - Module-based service registration using extension methods
  - Interface segregation with dedicated abstractions per layer
  - Transient lifetime for stateless services

  Technology Stack

  - .NET Core with minimal API configuration
  - ASP.NET Core controllers with attribute routing
  - Docker with health checks and environment configuration
  - Swagger/OpenAPI for API documentation

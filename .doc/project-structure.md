[Back to README](../README.md)

## Project Structure

The project should be structured as follows:

```
root
Ambev.DeveloperEvaluation
│
├── Adapters
│   ├── Driven
│   │   └── Infrastructure
│   │       └── Ambev.DeveloperEvaluation.ORM      -> ORM & database layer
│   └── Drivers
│       └── WebApi
│           └── Ambev.DeveloperEvaluation.WebApi   -> Presentation layer (controllers, swagger, startup)
│
├── Core
│   ├── Application
│   │   └── Ambev.DeveloperEvaluation.Application  -> Application services, DTOs, use cases
│   └── Domain
│       └── Ambev.DeveloperEvaluation.Domain       -> Entities, aggregates, domain logic
│
├── Crosscutting
│   ├── Ambev.DeveloperEvaluation.Common           -> Shared utilities, constants, helpers
│   └── Ambev.DeveloperEvaluation.IoC              -> Dependency injection, service registration
│
├── Tests
│   ├── Functional
│   │   └── Ambev.DeveloperEvaluation.Functional   -> Functional tests (end-to-end scenarios)
│   ├── Integration
│   │   └── Ambev.DeveloperEvaluation.Integration  -> Integration tests with external dependencies
│   ├── Shared
│   │   └── Ambev.DeveloperEvaluation.Support      -> Test utilities and shared setup
│   └── Unit
│       └── Ambev.DeveloperEvaluation.Unit         -> Unit tests for domain and application services
│
└── docker-compose                                 -> Orchestration file for containers
└── README.md
```

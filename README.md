
```markdown
# Pixel Service Management API

## Overview

The Pixel Service Management API focusing on tracking website visits via an embedded pixel. Leveraging the power of .NET 7 and Minimal APIs, the project architecture is built upon Domain-Driven Design (DDD), Command Query Responsibility Segregation (CQRS), and the MediatR pattern, ensuring scalability and maintainability.

## Features

- **PixelAPI Producer Service**: Captures and forwards essential visit data, such as Referrer and User-Agent headers, along with the visitor's IP address.
- **Storage Service**: Logs the received data in an append-only file, ensuring data persistence and retrievability for analysis.

## Getting Started

### Prerequisites

- Docker
- .NET 7 SDK

### Running the Services

To launch the services, execute the following command:

```bash
docker compose up -d --build
```

### Accessing the API

Explore the API via Swagger at the following URL:

```
http://localhost:8000/swagger/index.html
```

## Technology Stack

- **.NET 7**: Provides a comprehensive framework for building web applications.
- **Minimal API**: Utilized for crafting HTTP APIs with minimal dependencies.
- **Swagger**: Facilitates API documentation and testing.
- **Docker Compose**: Simplifies the deployment and management of application services.
- **Carer Module**: for simplicity.
- **CQRS and MediatR**: Separates read and write operations, enhancing scalability and maintainability.
- **xUnit & TestServer**: Used for integration testing, ensuring reliability.
- **Fluent Validation**: Enforces validation rules, ensuring data integrity.





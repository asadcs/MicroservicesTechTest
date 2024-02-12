
```markdown
# Pixel Service Management API

## Overview

The Pixel Service Management API offers a robust solution designed for e-commerce analytics, focusing on tracking website visits via an embedded pixel. This solution is pivotal for facilitating CRUD operations within the "Product" domain. Leveraging the power of .NET 7 and Minimal APIs, the project architecture is built upon Domain-Driven Design (DDD), Command Query Responsibility Segregation (CQRS), and the MediatR pattern, ensuring scalability and maintainability.

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

```

### Installation

1. Clone the repository:
   ```
   git clone <repository-url>
   ```
2. Navigate to the project directory:
   ```
   cd PixelServiceManagementAPI
   ```
3. Start the application:
   ```
   docker compose up -d --build
   ```

## Contributing

We welcome contributions! Please read our [Contributing Guide](CONTRIBUTING.md) for details on how to submit pull requests and suggestions.

## License

This project is licensed under the [MIT License](LICENSE).

## Acknowledgments

- The .NET Community
- Contributors and maintainers of the MediatR library
- Everyone who has provided feedback and suggestions
```

Remember to replace `<repository-url>` with the actual URL of your GitHub repository. Additionally, if you have contributing guidelines or a license file, you should include them in your repository and link them appropriately in the README.

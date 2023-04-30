# Company API - ReadMe

## Table of Contents

1. [General Information](#general-information)
2. [Technologies](#technologies)
3. [Features](#features)
4. [Setup](#setup)
5. [Usage](#usage)
6. [Tests](#tests)
7. [Monitoring and Observability](#monitoring-and-observability)

## General Information

The Company API is a RESTful API built using .NET 5, providing an efficient and scalable solution for managing company data. It follows Clean Architecture principles to ensure separation of concerns and modularity. Security features such as CORS and JWT authentication have been implemented to protect the API from unauthorized access.

## Technologies

The Company API project has been developed using the following technologies:

- **RESTful API with .NET 5:** The API is built using the .NET 5 framework, which provides high-performance and cross-platform capabilities.
- **Clean Architecture:** The project follows the Clean Architecture pattern, with separate layers for Controllers, UseCases, and Repositories.
- **Security:** The API is secured using CORS and JWT token authentication to prevent unauthorized access.
- **Integration and Unit Tests:** The project includes a comprehensive suite of integration and unit tests, utilizing xUnit, Bogus, and NSubstitute.
- **Test Coverage with Stryker:** Test coverage is analyzed using the Stryker mutation testing tool to ensure code quality.
- **HealthChecks:** HealthChecks are implemented to monitor the health of the API and its dependencies.
- **Observability with Prometheus:** Prometheus is used for monitoring and observability of the API's performance and resource usage.
- **In-Memory SQL Database:** The API uses an in-memory SQL database for fast and efficient data storage.
- **Docker Support:** The project includes Docker support for easy containerization and deployment.

## Features

The Company API provides the following features:

- Create, read, update, and delete (CRUD) operations for companies and their related data.
- Secure authentication using JWT tokens.
- Role-based authorization for different user types.
- Comprehensive testing with high test coverage.
- Health monitoring and performance metrics.

## Setup

To set up the Company API, follow these steps:

1. Ensure you have Docker installed on your machine.
2. Clone the repository to your local machine.
3. Navigate to the `CompanyAPI` folder in your terminal or command prompt.
4. Run the following command to start the application using Docker:

```sh
docker-compose up
```

The application will start on `localhost:5000`. Access the API documentation via Swagger at: http://localhost:5000/swagger/index.html

## Usage

After setting up the application, you can interact with the API using any REST client, such as Postman or the Swagger UI. Some common operations include:

- Authenticate and obtain a JWT token.
- Perform CRUD operations for companies and related data.
- Filter and sort company data based on specific criteria.

## Tests

The Company API includes both integration and unit tests. To run the tests, follow these steps:

1. Navigate to the `tests` folder in your terminal or command prompt.
2. Run the following command to execute all tests:

```sh
dotnet test
```

3. Optionally, you can generate a test coverage report using Stryker by running:

```sh
dotnet stryker
```

## Monitoring and Observability

The Company API implements HealthChecks and Prometheus for monitoring and observability. You can access the following endpoints to view metrics and monitor the health of the API:

- HealthChecks: http://localhost:5000/health
- Prometheus Metrics: http://localhost:5000/metrics

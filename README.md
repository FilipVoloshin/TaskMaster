# Task Master API

This project is a task management system that allows users to create, assign, and manage tasks. The system is built using CQRS (Command Query Responsibility Segregation) to separate read and write operations, improving the maintainability and scalability of the application. The project uses the following technologies and libraries:

* ASP.NET Core for building the API.
* Entity Framework Core for database access.
* PostgreSQL as the database system.
* MediatR for implementing the Mediator pattern.
* Ardalis.Specification for creating and executing specifications against the data store.
* AutoMapper for object-to-object mapping.
* FluentValidation for validation of request objects.

## Getting Started

### Prerequisites
* Docker
* .NET SDK
* PostgreSQL client (e.g., pgAdmin or DBeaver)

### Setting Up the Development Environment
1) Clone the repository:
```
git clone [repository_url]
cd [repository_name]
```

2) Start a PostgreSQL container using Docker:
```
docker pull postgres
docker run --name taskmaster-postgres -e POSTGRES_PASSWORD=your_password -p 5433:5433 -d postgres:latest
```

3) Connect to the PostgreSQL container using a PostgreSQL client (e.g., pgAdmin or DBeaver). Use the following connection details:
* Host: localhost
* Port: 5433
* User: postgres
* Password: The password you set in step 2

4) Create a new database and two users (one for read operations and another for write operations):

```sql
CREATE DATABASE task-master;
CREATE USER read_user WITH PASSWORD 'read_user_password';
CREATE USER write_user WITH PASSWORD 'write_user_password';

GRANT CONNECT ON DATABASE task-master TO read_user;
GRANT CONNECT ON DATABASE task-master TO write_user;

\c task-master;

GRANT USAGE ON SCHEMA public TO read_user;
GRANT USAGE ON SCHEMA public TO write_user;

ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT ON TABLES TO read_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT INSERT, UPDATE, DELETE ON TABLES TO write_user;
```
Replace **read_user_password** and **write_user_password** with secure passwords.

5) Update the `appsettings.json` file in the project with the appropriate connection strings for the read and write users:
```json
{
  "ConnectionStrings": {
    "CommandPostgreSQLConnection": "Server=localhost;Port=5433;Database=task-master;User Id=write_user;Password=write_user_password;",
    "QueryPostgreSQLConnection": "Server=localhost;Port=5433;Database=task-master;User Id=read_user;Password=read_user_password;"
  }
}
```
6) Run the project `dotnet run --project path/to/your/api/project`
The API should now be running at *http://localhost:5000* or *https://localhost:5001*.

## Key Concepts
### CQRS 
Command Query Responsibility Segregation is a pattern that separates the read and write operations in an application. Commands are responsible for modifying data, while queries are responsible for retrieving data. This separation of concerns enables better maintainability, scalability, and performance optimization.

### MediatR
MediatR is a library that implements the Mediator pattern, simplifying communication between various components in the application. It promotes loose coupling by allowing objects to send and receive messages without knowing about each other's existence.

### Ardalis.Specification
Ardalis.Specification is a library that helps create and execute specifications against the data store. Specifications are a way to encapsulate query logic and make it reusable. This library simplifies the process of writing and executing complex queries with Entity Framework Core.

### AutoMapper
AutoMapper is a library that provides a simple way to map one object to another. It simplifies the process of creating Data Transfer Objects (DTOs) and mapping between domain models and DTOs.

### FluentValidation
FluentValidation is a library that provides a simple way to implement validation rules for request objects. It uses a fluent API to define validation rules, making the code more readable and maintainable.

### REST
Representational State Transfer is an architectural style for building web services. It uses a client-server model and stateless communication between them. *REST* is based on HTTP and its verbs, where resources are identified by a unique URL. It supports multiple formats such as XML and JSON, and is used to build scalable and highly-performant web applications.

### RESTful: 
A RESTful web service is an implementation of *REST* principles, which follows a set of constraints, including being stateless, using a client-server architecture, caching, and using HTTP verbs. It also uses hypermedia as the engine of application state (HATEOAS), which means that the server provides links to related resources, allowing the client to navigate the API. RESTful APIs are flexible and can be used to build a wide range of web applications.

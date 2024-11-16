# ECommerce Platform

## Overview
ECommerce is a modular and scalable e-commerce platform built with .NET 8.0. The project implements best practices, including a layered architecture, FluentValidation, and Global Exception Middleware. 

## Features
- Layered architecture (Data Access, Business, WebAPI layers).
- FluentValidation for input validation.
- Global exception handling middleware.
- Authentication via JWT.
- Docker-ready configuration (future integration).
- Entity Framework Core for data persistence.
- API documentation with Swagger.

## Requirements
- **.NET 8.0 SDK**
- **SQL Server** (or LocalDB for development)
- **Docker** (optional, for containerization)

## Getting Started
1. Clone the repository:
   ```bash
   git clone https://github.com/[kullanıcı_adınız]/ECommerce.git
   ```
2. Configure the connection string in `appsettings.json`.
3. Run the migrations:
   ```bash
   dotnet ef database update
   ```
4. Start the application:
   ```bash
   dotnet run --project ECommerce.WebAPI
   ```

## Future Enhancements
- Full Docker support.
- Kubernetes integration.
- Enhanced UI/UX.

## License
This project is licensed under the MIT License.

# MedicalCare

A medical tests management application built with .NET 9.0 layered architecture.

## Project Overview

- **Purpose:** Manage medical tests and test categories with full CRUD operations
- **Layers:** Clean separation between Presentation (web), Application (use-cases), Domain (entities), and Infrastructure (EF Core, persistence)

## Architecture

- **Presentation:** `MedicalCare.Presentation` — ASP.NET Core MVC site with controllers and views
- **Application:** `MedicalCare.Application` — Application services, features, and repository interfaces
- **Domain:** `MedicalCare.Domain` — Domain entities and value objects (`Test`, `TestCategory`, policies)
- **Infrastructure:** `MedicalCare.Infrastructure` — EF Core DbContext, migrations, and repository implementations

## Tech Stack

- .NET 9.0 SDK
- ASP.NET Core MVC
- Entity Framework Core with migrations
- SQL Server 2022
- Docker & Docker Compose

## Prerequisites

Choose one of the following options:

### Option 1: Docker (Recommended - Easiest)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) installed and running

### Option 2: Local Development
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) installed
- SQL Server 2022 (local instance or Docker container)
- EF Core CLI tools

## Quick Start with Docker (Recommended)

This is the easiest way to run the application. Docker Compose will automatically set up both the database and the application.

1. **Clone the repository:**
   ```bash
   git clone <repository-url>
   cd MedicalCare
   ```

2. **Start the application:**
   ```bash
   docker-compose up --build
   ```

3. **Access the application:**
   - Open your browser to `http://localhost:8080`
   - The database will be automatically created and seeded with test categories

4. **Stop the application:**
   ```bash
   docker-compose down
   ```

   To remove database data as well:
   ```bash
   docker-compose down -v
   ```

## Local Development Setup

### Step 1: Install Prerequisites

1. **Install .NET 9.0 SDK:**
   ```bash
   dotnet --version  # Should show 9.0.x
   ```

2. **Install EF Core CLI tools:**
   ```bash
   dotnet tool install --global dotnet-ef
   dotnet ef --version
   ```

3. **Set up SQL Server:**

   Using Docker:
   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=StrongPass@123" \
     -p 1433:1433 --name medicalcare-sql \
     -d mcr.microsoft.com/mssql/server:2022-latest
   ```

### Step 2: Configure Database Connection

The default connection string in `MedicalCare.Presentation/appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=MedicalCareDb;User Id=sa;Password=StrongPass@123;TrustServerCertificate=True;"
}
```

**Important:** If using a different SQL Server instance, update the connection string accordingly.

### Step 3: Apply Database Migrations

From the repository root:

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Apply migrations (creates database and seeds data)
dotnet ef database update --project MedicalCare.Infrastructure --startup-project MedicalCare.Presentation
```

### Step 4: Run the Application

```bash
dotnet run --project MedicalCare.Presentation
```

The application will start on `http://localhost:5000` or `https://localhost:5001` (check console output for exact URLs).

## Database Migrations

Migrations are located in `MedicalCare.Infrastructure/Migrations/` and include:
- Initial schema creation
- Test categories seeding (added Jan 2026)

### Common Migration Commands

```bash
# Apply all pending migrations
dotnet ef database update --project MedicalCare.Infrastructure --startup-project MedicalCare.Presentation

# Create a new migration
dotnet ef migrations add MigrationName --project MedicalCare.Infrastructure --startup-project MedicalCare.Presentation

# Rollback to a specific migration
dotnet ef database update PreviousMigrationName --project MedicalCare.Infrastructure --startup-project MedicalCare.Presentation
```

## Troubleshooting

### Docker Issues

**Problem:** Port 1433 or 8080 already in use
```bash
# Find and stop conflicting services
docker ps
docker stop <container-id>

# Or use different ports in docker-compose.yml
```

**Problem:** Docker build fails
```bash
# Clean Docker cache and rebuild
docker-compose down -v
docker system prune -a
docker-compose up --build
```

### Database Connection Issues

**Problem:** Cannot connect to SQL Server
- Verify SQL Server is running: `docker ps` (if using Docker)
- Check connection string in `appsettings.json`
- Ensure password meets SQL Server complexity requirements
- Verify port 1433 is accessible

**Problem:** Migration fails
```bash
# Drop and recreate database
dotnet ef database drop --project MedicalCare.Infrastructure --startup-project MedicalCare.Presentation
dotnet ef database update --project MedicalCare.Infrastructure --startup-project MedicalCare.Presentation
```

### .NET Version Issues

**Problem:** Wrong .NET version
```bash
# Check installed versions
dotnet --list-sdks

# Install .NET 9.0 from: https://dotnet.microsoft.com/download/dotnet/9.0
```

## Project Structure

```
MedicalCare/
├── MedicalCare.Presentation/     # ASP.NET Core MVC application
├── MedicalCare.Application/      # Business logic and interfaces
├── MedicalCare.Domain/           # Domain entities and policies
├── MedicalCare.Infrastructure/   # EF Core, repositories, migrations
├── Dockerfile                    # Application container definition
├── docker-compose.yml            # Multi-container orchestration
└── README.md                     # This file
```

## Key Features

- Medical test management (CRUD operations)
- Test category management with seeded data
- Clean architecture with separated concerns
- Entity Framework Core with Code-First migrations
- Docker support for easy deployment
- SQL Server database with automatic schema creation

## Configuration Files

- `MedicalCare.Presentation/appsettings.json` — Connection strings and logging
- `MedicalCare.Presentation/Program.cs` — Application startup and DI configuration
- `docker-compose.yml` — Docker services configuration
- `Dockerfile` — Multi-stage build for the application

## Development Workflow

1. Make code changes in your preferred IDE
2. Create migrations if database schema changes:
   ```bash
   dotnet ef migrations add YourMigrationName --project MedicalCare.Infrastructure --startup-project MedicalCare.Presentation
   ```
3. Apply migrations:
   ```bash
   dotnet ef database update --project MedicalCare.Infrastructure --startup-project MedicalCare.Presentation
   ```
4. Test locally before committing

## Security Notes

- **Default password** (`StrongPass@123`) is for development only
- Use environment variables or Azure Key Vault for production secrets
- Configure user secrets for local development:
  ```bash
  dotnet user-secrets init --project MedicalCare.Presentation
  dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-connection-string" --project MedicalCare.Presentation
  ```

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -m 'Add some feature'`)
4. Push to the branch (`git push origin feature/your-feature`)
5. Open a Pull Request

## License

This is a sample application for educational purposes.

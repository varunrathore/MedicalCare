# MedicalCare

A simple medical tests management sample built with .NET layered architecture.

## Project overview

- **Purpose:** A sample application to manage medical tests and test categories.
- **Layers:** Clear separation between Presentation (web), Application (use-cases), Domain (entities), and Infrastructure (EF Core, persistence).

## Architecture

- **Presentation:** `MedicalCare.Presentation` — ASP.NET Core MVC site, hosts controllers, views and the startup program.
- **Application:** `MedicalCare.Application` — application services, features, and repository interfaces.
- **Domain:** `MedicalCare.Domain` — domain entities and value objects (e.g. `Test`, `TestCategory`).
- **Infrastructure:** `MedicalCare.Infrastructure` — EF Core DbContext, migrations, repositories and DI wiring.

Project layout (top-level folders):

- `MedicalCare.Presentation/` — web app and `Program.cs`.
- `MedicalCare.Application/` — business logic and interfaces.
- `MedicalCare.Domain/` — entities and domain models.
- `MedicalCare.Infrastructure/` — persistence, `Migrations/`, and repository implementations.

## Tech stack

- .NET (C#) — multi-project solution
- ASP.NET Core MVC
- Entity Framework Core (EF Core) migrations
- SQL Server (connection string in `MedicalCare.Presentation/appsettings.json`)
- Docker / Docker Compose (optional, `docker-compose.yml` available)

## Database & Migrations

Migrations are located in `MedicalCare.Infrastructure/Migrations`.
To apply migrations locally you can either run the EF CLI or run the web app (if it applies migrations at startup).

Install the EF CLI if you don't have it:

```bash
dotnet tool install --global dotnet-ef
```

From the repository root, apply migrations with:

```bash
dotnet restore
dotnet build
dotnet ef database update --project MedicalCare.Infrastructure --startup-project MedicalCare.Presentation
```

- `--project` points to the project containing migrations.
- `--startup-project` points to the project that provides the application services & configuration (usually the Presentation project).

Connection string is configured in `MedicalCare.Presentation/appsettings.json` under `ConnectionStrings:DefaultConnection`.

## Run locally (dotnet CLI)

1. Ensure you have the .NET SDK installed (recommended latest 6/7/8 depending on the solution target).
2. Ensure an accessible SQL Server instance is running (local or container) matching the connection string in `appsettings.json`.

Commands to restore, build and run the Presentation project:

```bash
dotnet restore
dotnet build
dotnet run --project MedicalCare.Presentation
```

The app will start on the configured Kestrel ports (see `Properties/launchSettings.json` or console output).

## Run with Docker Compose

If you prefer containerized local development, use the provided `docker-compose.yml` to start services (including DB) and the app:

```bash
docker-compose up --build
```

Check `docker-compose.yml` and `MedicalCare.Presentation/appsettings.json` to confirm the SQL Server settings and ports.

## Notes on configuration

- Sensitive values (DB passwords, secrets) should be stored in environment variables or user secrets for development.
- The default `appsettings.json` contains an example `DefaultConnection` entry — adjust to your environment.

## Tests

If there are unit/integration tests in the `MedicalCare.Application` or other projects, run them with:

```bash
dotnet test
```

## Contributing

- Fork the repo, create a feature branch, run tests, and open a pull request.

## Files to inspect

- `MedicalCare.Infrastructure/Migrations/` — EF Core migrations
- `MedicalCare.Presentation/appsettings.json` — connection strings & logging
- `MedicalCare.Presentation/Program.cs` — application startup

If you'd like, I can also:

- Add a badge matrix or CI instructions.
- Patch `Program.cs` to automatically apply migrations at startup.
- Add a small launch/test script to simplify local runs.

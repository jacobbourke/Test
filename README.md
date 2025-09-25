# Accounting Platform Skeleton

This repository bootstraps the architecture for a Xero/QuickBooks style accounting platform written in C#/.NET. It establishes a multi-project solution with an API, core domain library, infrastructure helpers, and a bootstrapper CLI that can be targeted to either cloud or customer-hosted deployments.

## Solution layout

- `Accounting.Platform.sln` – solution tying all projects together.
- `src/Accounting.Platform.Core` – core domain primitives such as deployment profiles.
- `src/Accounting.Platform.Infrastructure` – infrastructure helpers and configuration extensions.
- `src/Accounting.Platform.Api` – minimal web API showcasing dependency injection and deployment-aware endpoints.
- `src/Accounting.Platform.Bootstrapper` – console app guiding teams through environment-specific setup.

## Deployment configuration

Deployment defaults to **Local** hosting. Override by either setting the `DEPLOYMENT_MODE` environment variable or providing `Deployment:Mode` via configuration (e.g. `appsettings.Cloud.json`). The API automatically loads the corresponding configuration file and exposes the active profile at `/deployment/profile`.

## Getting started

1. Install the .NET 8 SDK.
2. Restore the solution:
   ```bash
   dotnet restore Accounting.Platform.sln
   ```
3. Run the bootstrapper to verify the desired deployment mode:
   ```bash
   dotnet run --project src/Accounting.Platform.Bootstrapper -- --Deployment:Mode=Cloud
   ```
4. Launch the API:
   ```bash
   dotnet run --project src/Accounting.Platform.Api
   ```
5. Query the health endpoint to confirm the selected deployment mode:
   ```bash
   curl http://localhost:5000/health
   ```

## Next steps

- Flesh out domain services (ledger, invoicing, banking) inside `Accounting.Platform.Core`.
- Add persistence, messaging, and integration adapters within `Accounting.Platform.Infrastructure`.
- Expand the API into modular endpoints or microservices as needed.
- Introduce automated tests and CI/CD pipelines to enforce quality and deployment safety.

# RedisDiagnostic

.NET 8 library that wraps `StackExchange.Redis.IDatabase` with Prometheus duration metrics. A Roslyn source generator emits a complete `InstrumentedDatabase` decorator.

## GitHub Flow

- `develop` is the default integration branch and is always releasable.
- Do work on a short-lived branch from `develop` (`cursor/…` or `feature/…`).
- Open a pull request into `develop`. CI must pass before merge.
- Do not commit directly to `develop`.

## Build and test

```bash
dotnet build RedisDiagnostic.sln
dotnet test tests/RedisDiagnostic.Tests
dotnet test tests/RedisDiagnostic.FunctionalTests   # Docker required
```

## Sample host

Needs Redis on `localhost:6379` (or `Redis:ConnectionString`):

```bash
dotnet run --project src/RedisDiagnostic.Host
```

- CRUD: `PUT/GET/DELETE /items/{key}`, `GET /items/{key}/exists`, `PUT/GET /hashes/{key}/{field}`
- Metrics: `GET /metrics`

# ApiVersioningTest

A .NET 9 Web API that demonstrates ASP.NET API versioning using the `Asp.Versioning` library. The same endpoint is exposed under multiple API versions (v1, v2, v3) with different implementations, showcasing URL segment versioning and media type version reading.

## Technologies

- .NET 9
- ASP.NET Core Minimal APIs
- Asp.Versioning.Http 8.1.0
- Asp.Versioning.Mvc.ApiExplorer 8.1.0
- Swashbuckle / Swagger UI

## Key Features

- **URL path versioning**: Endpoints are mapped under `api/v{version:apiVersion}/returnvalue`.
- **Media type version reader**: Clients can also specify the version via the `ver` media type parameter.
- **Version reporting**: The API reports supported and deprecated versions in response headers.
- **Deprecated version**: v1 is marked as deprecated while v2 and v3 are active.
- **Swagger documentation**: Auto-generated Swagger docs with version-specific endpoint grouping.
- **Default version fallback**: Assumes v1 when no version is specified.

## Endpoints

| Method | Path | Description |
|--------|------|-------------|
| GET | `/api/v1/returnvalue` | Returns "Some value v1" (deprecated) |
| GET | `/api/v2/returnvalue` | Returns "Some value v2" |
| GET | `/api/v3/returnvalue` | Returns "Some value v3" |

## How to Run

```bash
dotnet run --project ApiVersioningTest.csproj
```

Navigate to `http://localhost:<port>` to access the Swagger UI and test different API versions.

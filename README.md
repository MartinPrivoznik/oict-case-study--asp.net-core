# OICT Case Study — ASP.NET Core

## Technical Overview

|                   |                                                       |
|-------------------|-------------------------------------------------------|
| Framework         | ASP.NET Core 10 (.NET 10)                             |
| Architecture      | Layered — Api / App / ApiClient                       |
| API versioning    | URL segment (`/api/v1/…`) via `Asp.Versioning` 10.0.0 |
| Mediator          | MediatR 14.1.0                                        |
| API documentation | Swagger / OpenAPI via Swashbuckle 10.1.7              |
| Authentication    | API key — `X-Api-Key` request header                  |
| External API      | Litacka API (card validity and state)                 |

## Running the Application

### IDE run configurations

The project includes four run configurations defined in `src/OICTCaseStudy.Api`:

| Profile                | URL                    | Notes                     |
|------------------------|------------------------|---------------------------|
| `http`                 | http://localhost:5092  | Development environment   |
| `https`                | https://localhost:7223 | Development environment   |
| `Docker (Development)` | http://localhost:8080  | Docker, development build |
| `Docker (Production)`  | http://localhost:8080  | Docker, production build  |

Select the desired profile in your IDE and run the `OICTCaseStudy.Api` project.

In all configurations the following endpoints are available at the base URL:

| Endpoint     | Path       |
|--------------|------------|
| Health check | `/health`  |
| Swagger UI   | `/swagger` |

The Swagger UI includes an **Authorize** button for setting the `X-Api-Key` header required by protected endpoints. The
key can be found in:

- `src/OICTCaseStudy.Api/appsettings.Development.json` — development build
- `.env` (decrypt first, see below) — production build

### Docker Compose — development

The development build uses `appsettings.Development.json` for configuration. No additional setup is required.

```bash
docker compose -f compose.development.yaml up --build
```

The API will be available at http://localhost:8080.

### Docker Compose — production

The production build reads secrets and settings from the `.env` file located at the repository root. This file is
encrypted with [git-crypt](https://github.com/AGWA/git-crypt) and must be decrypted before use.

**Decrypting the `.env` file**

1. Obtain the git-crypt key file from the project maintainer.
2. Unlock the repository:

```bash
git-crypt unlock /path/to/keyfile
```

**Starting the production build**

```bash
docker compose up --build
```

The API will be available at http://localhost:8080.

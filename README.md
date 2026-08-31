[![](https://img.shields.io/nuget/v/soenneker.cloudflare.registrar.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.cloudflare.registrar/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cloudflare.registrar/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.cloudflare.registrar/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.cloudflare.registrar.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.cloudflare.registrar/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cloudflare.registrar/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.cloudflare.registrar/actions/workflows/codeql.yml)

# Soenneker.Cloudflare.Registrar

Searches, registers, and manages domains through Cloudflare Registrar using the generated Cloudflare API client.

## Installation

```bash
dotnet add package Soenneker.Cloudflare.Registrar
```

## Configuration

```json
{
  "Cloudflare": {
    "ApiKey": "your-api-token"
  }
}
```

The token must be authorized for Cloudflare Registrar operations on the target account. Store it in a secret provider rather than source control.

## Registration

```csharp
using Soenneker.Cloudflare.Registrar.Registrars;

services.AddCloudflareRegistrarUtilAsScoped();
```

Singleton registration is also available with `AddCloudflareRegistrarUtilAsSingleton()`.

## Search and availability

```csharp
using Soenneker.Cloudflare.Registrar.Abstract;
using Soenneker.Cloudflare.OpenApiClient.Models;

public sealed class RegistrarService
{
    private readonly ICloudflareRegistrarUtil _registrarUtil;

    public RegistrarService(ICloudflareRegistrarUtil registrarUtil)
    {
        _registrarUtil = registrarUtil;
    }

    public async Task Search(string accountId, CancellationToken cancellationToken)
    {
        var result = await _registrarUtil.SearchDomains(accountId, config =>
        {
            config.QueryParameters.Q = "example";
            config.QueryParameters.Limit = 10;
        }, cancellationToken);
    }
}
```

`SearchDomains` accepts the generated query parameters, including result limits. `CheckDomain` accepts a `RegistrarApiDomainCheckRequest` when a dedicated availability check is preferable.

## Registrations

Use `CreateRegistration` to submit a `RegistrarApiRegistrationCreateRequest`, then inspect the returned workflow status. `GetRegistrationStatus` reports registration progress; `GetUpdateStatus` reports changes submitted through `UpdateRegistration`.

```csharp
RegistrarApiRegistrationResponseSingle? registration =
    await registrar.GetRegistration(accountId, "example.com", cancellationToken: cancellationToken);
```

Registration and contact updates are consequential remote operations. Validate request models before submitting them, handle generated API exceptions, and do not treat the initial workflow response as proof that the operation has finished.

## Direct generated-client access

`Get(accountId)` returns the generated `RegistrarRequestBuilder` for endpoints or request options not wrapped by this package. Response envelopes are nullable because the generated client can represent an empty response body.

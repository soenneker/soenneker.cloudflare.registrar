[![](https://img.shields.io/nuget/v/soenneker.cloudflare.registrar.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.cloudflare.registrar/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cloudflare.registrar/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.cloudflare.registrar/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.cloudflare.registrar.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.cloudflare.registrar/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Cloudflare.Registrar
### A utility for managing Cloudflare Registrar

## Installation

```
dotnet add package Soenneker.Cloudflare.Registrar
```

## Usage

```csharp
using Soenneker.Cloudflare.Registrar.Registrars;

services.AddCloudflareRegistrarUtilAsScoped();
```

```csharp
using Soenneker.Cloudflare.Registrar.Abstract;

public sealed class RegistrarService
{
    private readonly ICloudflareRegistrarUtil _registrarUtil;

    public RegistrarService(ICloudflareRegistrarUtil registrarUtil)
    {
        _registrarUtil = registrarUtil;
    }

    public async Task Search(string accountId, CancellationToken cancellationToken = default)
    {
        var result = await _registrarUtil.SearchDomains(accountId, config =>
        {
            config.QueryParameters.Q = "example";
            config.QueryParameters.Limit = 10;
        }, cancellationToken);
    }
}
```

`ICloudflareRegistrarUtil.Get(accountId)` returns the generated Cloudflare Registrar request builder for direct access to any registrar endpoint exposed by `Soenneker.Cloudflare.OpenApiClient`.

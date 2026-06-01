using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Cloudflare.Registrar.Abstract;

namespace Soenneker.Cloudflare.Registrar.Registrars;

/// <summary>
/// A utility for managing Cloudflare Registrar
/// </summary>
public static class CloudflareRegistrarUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="ICloudflareRegistrarUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddCloudflareRegistrarUtilAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<ICloudflareRegistrarUtil, CloudflareRegistrarUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="ICloudflareRegistrarUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddCloudflareRegistrarUtilAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<ICloudflareRegistrarUtil, CloudflareRegistrarUtil>();

        return services;
    }
}

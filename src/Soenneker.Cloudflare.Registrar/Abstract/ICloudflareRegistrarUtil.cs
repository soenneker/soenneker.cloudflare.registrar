namespace Soenneker.Cloudflare.Registrar.Abstract;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions;
using Soenneker.Cloudflare.OpenApiClient.Accounts.Item.Registrar;
using Soenneker.Cloudflare.OpenApiClient.Accounts.Item.Registrar.DomainSearch;
using Soenneker.Cloudflare.OpenApiClient.Accounts.Item.Registrar.Registrations;
using Soenneker.Cloudflare.OpenApiClient.Models;

/// <summary>
/// A utility for managing Cloudflare Registrar
/// </summary>
public interface ICloudflareRegistrarUtil
{
    /// <summary>
    /// Gets the Cloudflare Registrar request builder for an account.
    /// </summary>
    ValueTask<RegistrarRequestBuilder> Get(string accountId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Search for domains available through Cloudflare Registrar.
    /// </summary>
    ValueTask<RegistrarApiDomainSearchResponse?> SearchDomains(string accountId,
        Action<RequestConfiguration<DomainSearchRequestBuilder.DomainSearchRequestBuilderGetQueryParameters>>? requestConfiguration = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Check domain availability through Cloudflare Registrar.
    /// </summary>
    ValueTask<RegistrarApiDomainCheckResponse?> CheckDomain(string accountId, RegistrarApiDomainCheckRequest request,
        Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// List registrations for a Cloudflare account.
    /// </summary>
    ValueTask<RegistrarApiRegistrationResponseCollection?> GetRegistrations(string accountId,
        Action<RequestConfiguration<RegistrationsRequestBuilder.RegistrationsRequestBuilderGetQueryParameters>>? requestConfiguration = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a domain registration.
    /// </summary>
    ValueTask<RegistrarApiWorkflowStatusResponseSingle?> CreateRegistration(string accountId, RegistrarApiRegistrationCreateRequest request,
        Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a domain registration.
    /// </summary>
    ValueTask<RegistrarApiRegistrationResponseSingle?> GetRegistration(string accountId, string domainName,
        Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a domain registration.
    /// </summary>
    ValueTask<RegistrarApiWorkflowStatusResponseSingle?> UpdateRegistration(string accountId, string domainName, RegistrarApiRegistrationUpdateRequest request,
        Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the registration workflow status for a domain.
    /// </summary>
    ValueTask<RegistrarApiWorkflowStatusResponseSingle?> GetRegistrationStatus(string accountId, string domainName,
        Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the update workflow status for a domain.
    /// </summary>
    ValueTask<RegistrarApiWorkflowStatusResponseSingle?> GetUpdateStatus(string accountId, string domainName,
        Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = null, CancellationToken cancellationToken = default);
}

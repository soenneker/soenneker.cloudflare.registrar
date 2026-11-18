using Soenneker.Cloudflare.Registrar.Abstract;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions;
using Soenneker.Cloudflare.OpenApiClient.Accounts.Item.Registrar;
using Soenneker.Cloudflare.OpenApiClient.Accounts.Item.Registrar.DomainSearch;
using Soenneker.Cloudflare.OpenApiClient.Accounts.Item.Registrar.Registrations;
using Soenneker.Cloudflare.OpenApiClient.Models;
using Soenneker.Cloudflare.Utils.Client.Abstract;
using Soenneker.Extensions.Task;
using Soenneker.Extensions.ValueTask;

namespace Soenneker.Cloudflare.Registrar;

/// <inheritdoc cref="ICloudflareRegistrarUtil" />
public sealed class CloudflareRegistrarUtil : ICloudflareRegistrarUtil
{
    private readonly ICloudflareClientUtil _cloudflareClientUtil;

    public CloudflareRegistrarUtil(ICloudflareClientUtil cloudflareClientUtil)
    {
        _cloudflareClientUtil = cloudflareClientUtil;
    }

    public async ValueTask<RegistrarRequestBuilder> Get(string accountId, CancellationToken cancellationToken = default)
    {
        var client = await _cloudflareClientUtil.Get(cancellationToken).NoSync();

        return client.Accounts[accountId].Registrar;
    }

    public async ValueTask<RegistrarDomainSearchResponse?> SearchDomains(string accountId,
        Action<RequestConfiguration<DomainSearchRequestBuilder.DomainSearchRequestBuilderGetQueryParameters>>?
            requestConfiguration = null, CancellationToken cancellationToken = default)
    {
        RegistrarRequestBuilder registrar = await Get(accountId, cancellationToken).NoSync();

        return await registrar.DomainSearch.GetAsync(requestConfiguration, cancellationToken).NoSync();
    }

    public async ValueTask<RegistrarDomainCheckResponse?> CheckDomain(string accountId,
        RegistrarDomainCheckRequest request,
        Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = null,
        CancellationToken cancellationToken = default)
    {
        RegistrarRequestBuilder registrar = await Get(accountId, cancellationToken).NoSync();

        return await registrar.DomainCheck.PostAsync(request, requestConfiguration, cancellationToken).NoSync();
    }

    public async ValueTask<RegistrarRegistrationResponseCollection?> GetRegistrations(string accountId,
        Action<RequestConfiguration<RegistrationsRequestBuilder.RegistrationsRequestBuilderGetQueryParameters>>?
            requestConfiguration = null, CancellationToken cancellationToken = default)
    {
        RegistrarRequestBuilder registrar = await Get(accountId, cancellationToken).NoSync();

        return await registrar.Registrations.GetAsync(requestConfiguration, cancellationToken).NoSync();
    }

    public async ValueTask<RegistrarWorkflowStatusResponseSingle?> CreateRegistration(string accountId,
        RegistrarRegistrationCreateRequest request,
        Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = null,
        CancellationToken cancellationToken = default)
    {
        RegistrarRequestBuilder registrar = await Get(accountId, cancellationToken).NoSync();

        return await registrar.Registrations.PostAsync(request, requestConfiguration, cancellationToken).NoSync();
    }

    public async ValueTask<RegistrarRegistrationResponseSingle?> GetRegistration(string accountId, string domainName,
        Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = null,
        CancellationToken cancellationToken = default)
    {
        RegistrarRequestBuilder registrar = await Get(accountId, cancellationToken).NoSync();

        return await registrar.Registrations[domainName].GetAsync(requestConfiguration, cancellationToken).NoSync();
    }

    public async ValueTask<RegistrarWorkflowStatusResponseSingle?> UpdateRegistration(string accountId,
        string domainName, RegistrarRegistrationUpdateRequest request,
        Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = null,
        CancellationToken cancellationToken = default)
    {
        RegistrarRequestBuilder registrar = await Get(accountId, cancellationToken).NoSync();

        return await registrar.Registrations[domainName].PatchAsync(request, requestConfiguration, cancellationToken)
                              .NoSync();
    }

    public async ValueTask<RegistrarWorkflowStatusResponseSingle?> GetRegistrationStatus(string accountId,
        string domainName, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = null,
        CancellationToken cancellationToken = default)
    {
        RegistrarRequestBuilder registrar = await Get(accountId, cancellationToken).NoSync();

        return await registrar.Registrations[domainName].RegistrationStatus
                              .GetAsync(requestConfiguration, cancellationToken).NoSync();
    }

    public async ValueTask<RegistrarWorkflowStatusResponseSingle?> GetUpdateStatus(string accountId,
        string domainName, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = null,
        CancellationToken cancellationToken = default)
    {
        RegistrarRequestBuilder registrar = await Get(accountId, cancellationToken).NoSync();

        return await registrar.Registrations[domainName].UpdateStatus.GetAsync(requestConfiguration, cancellationToken)
                              .NoSync();
    }
}

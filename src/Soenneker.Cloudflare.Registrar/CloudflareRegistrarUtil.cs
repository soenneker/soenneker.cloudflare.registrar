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

    public async ValueTask<RegistrarApiDomainSearchResponse?> SearchDomains(string accountId,
        Action<RequestConfiguration<DomainSearchRequestBuilder.DomainSearchRequestBuilderGetQueryParameters>>?
            requestConfiguration = null, CancellationToken cancellationToken = default)
    {
        RegistrarRequestBuilder registrar = await Get(accountId, cancellationToken).NoSync();

        return await registrar.DomainSearch.GetAsync(requestConfiguration, cancellationToken).NoSync();
    }

    public async ValueTask<RegistrarApiDomainCheckResponse?> CheckDomain(string accountId,
        RegistrarApiDomainCheckRequest request,
        Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = null,
        CancellationToken cancellationToken = default)
    {
        RegistrarRequestBuilder registrar = await Get(accountId, cancellationToken).NoSync();

        return await registrar.DomainCheck.PostAsync(request, requestConfiguration, cancellationToken).NoSync();
    }

    public async ValueTask<RegistrarApiRegistrationResponseCollection?> GetRegistrations(string accountId,
        Action<RequestConfiguration<RegistrationsRequestBuilder.RegistrationsRequestBuilderGetQueryParameters>>?
            requestConfiguration = null, CancellationToken cancellationToken = default)
    {
        RegistrarRequestBuilder registrar = await Get(accountId, cancellationToken).NoSync();

        return await registrar.Registrations.GetAsync(requestConfiguration, cancellationToken).NoSync();
    }

    public async ValueTask<RegistrarApiWorkflowStatusResponseSingle?> CreateRegistration(string accountId,
        RegistrarApiRegistrationCreateRequest request,
        Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = null,
        CancellationToken cancellationToken = default)
    {
        RegistrarRequestBuilder registrar = await Get(accountId, cancellationToken).NoSync();

        return await registrar.Registrations.PostAsync(request, requestConfiguration, cancellationToken).NoSync();
    }

    public async ValueTask<RegistrarApiRegistrationResponseSingle?> GetRegistration(string accountId, string domainName,
        Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = null,
        CancellationToken cancellationToken = default)
    {
        RegistrarRequestBuilder registrar = await Get(accountId, cancellationToken).NoSync();

        return await registrar.Registrations[domainName].GetAsync(requestConfiguration, cancellationToken).NoSync();
    }

    public async ValueTask<RegistrarApiWorkflowStatusResponseSingle?> UpdateRegistration(string accountId,
        string domainName, RegistrarApiRegistrationUpdateRequest request,
        Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = null,
        CancellationToken cancellationToken = default)
    {
        RegistrarRequestBuilder registrar = await Get(accountId, cancellationToken).NoSync();

        return await registrar.Registrations[domainName].PatchAsync(request, requestConfiguration, cancellationToken)
                              .NoSync();
    }

    public async ValueTask<RegistrarApiWorkflowStatusResponseSingle?> GetRegistrationStatus(string accountId,
        string domainName, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = null,
        CancellationToken cancellationToken = default)
    {
        RegistrarRequestBuilder registrar = await Get(accountId, cancellationToken).NoSync();

        return await registrar.Registrations[domainName].RegistrationStatus
                              .GetAsync(requestConfiguration, cancellationToken).NoSync();
    }

    public async ValueTask<RegistrarApiWorkflowStatusResponseSingle?> GetUpdateStatus(string accountId,
        string domainName, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = null,
        CancellationToken cancellationToken = default)
    {
        RegistrarRequestBuilder registrar = await Get(accountId, cancellationToken).NoSync();

        return await registrar.Registrations[domainName].UpdateStatus.GetAsync(requestConfiguration, cancellationToken)
                              .NoSync();
    }
}

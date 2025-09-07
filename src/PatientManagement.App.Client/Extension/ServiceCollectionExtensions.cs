using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using PatientManagement.App.Client.Api;
using PatientManagement.App.Domain.Interfaces;

namespace PatientManagement.App.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiClients(this IServiceCollection services, Uri baseAddress)
    {
        services.AddHttpClient<ISpecialityApiClient, SpecialityApiClient>(client =>
            client.BaseAddress = baseAddress);

        services.AddScoped<ISpecialityApiClient, SpecialityApiClient>();

        return services;
    }
}
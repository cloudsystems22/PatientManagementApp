using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using PatientManagement.App.Client.Api;
using PatientManagement.App.Domain.Interfaces;

namespace PatientManagement.App.Client.Extensions;

public static class ServiceCollectionExtensions
{
    private static string baseAddress = "http://localhost:8080";
    public static IServiceCollection AddApiClients(this IServiceCollection services)
    {
        services.AddScoped<ISpecialityApiClient, SpecialityApiClient>();
        services.AddScoped<IPatientApiClient, PatientApiClient>();

        services.AddHttpClient<ISpecialityApiClient, SpecialityApiClient>(client =>
            client.BaseAddress = new Uri(baseAddress));

        services.AddHttpClient<IPatientApiClient, PatientApiClient>(Client =>
            Client.BaseAddress = new Uri(baseAddress));


        return services;
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OICTCaseStudy.ApiClient.Litacka.Configuration;
using OICTCaseStudy.ApiClient.Litacka.HttpClient;
using OICTCaseStudy.ApiClient.Litacka.Services;

namespace OICTCaseStudy.ApiClient.Litacka;

public static class LitackaApiClientServiceCollection
{
    public static IServiceCollection AddLitackaApiClientServices(this IServiceCollection services,
        IConfigurationManager configuration)
    {
        // Configuration binding
        var litackaApiClientOptions = new LitackaApiClientOptions();
        var litackaApiClientConfig =
            configuration.GetSection(LitackaApiClientOptions.SectionName);
        litackaApiClientConfig.Bind(litackaApiClientOptions);
        services.Configure<LitackaApiClientOptions>(litackaApiClientConfig);

        services.AddHttpClient(litackaApiClientOptions.ApiClientName,
            client => { client.BaseAddress = new Uri(litackaApiClientOptions.UrlBase); });

        services.AddScoped<LitactkaHttpClient>();
        services.AddScoped<LitackaCardService>();

        return services;
    }
}
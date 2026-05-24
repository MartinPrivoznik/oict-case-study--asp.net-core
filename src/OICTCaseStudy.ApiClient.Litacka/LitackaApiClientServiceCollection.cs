using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using OICTCaseStudy.ApiClient.Litacka.Configuration;
using OICTCaseStudy.ApiClient.Litacka.HttpClient;
using OICTCaseStudy.ApiClient.Litacka.Services;
using Polly;

namespace OICTCaseStudy.ApiClient.Litacka;

public static class LitackaApiClientServiceCollection
{
    public static IServiceCollection AddLitackaApiClientServices(this IServiceCollection services,
        IConfigurationManager configuration)
    {
        var litackaApiClientConfig = configuration.GetSection(LitackaApiClientOptions.SectionName);
        services.Configure<LitackaApiClientOptions>(litackaApiClientConfig);

        var litackaApiClientOptions = litackaApiClientConfig.Get<LitackaApiClientOptions>()
                                      ?? new LitackaApiClientOptions();

        services.AddHttpClient<LitactkaHttpClient>(
                client => { client.BaseAddress = new Uri(litackaApiClientOptions.UrlBase); })
            .AddResilienceHandler("litacka-retry", builder =>
            {
                builder.AddRetry(new HttpRetryStrategyOptions
                {
                    MaxRetryAttempts = litackaApiClientOptions.RetryMaxAttempts,
                    Delay = litackaApiClientOptions.RetryDelay,
                    BackoffType = DelayBackoffType.Exponential,
                    UseJitter = true
                });
            });

        services.AddScoped<LitackaCardService>();

        return services;
    }
}
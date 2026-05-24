using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OICTCaseStudy.ApiClient.Litacka;

namespace OICTCaseStudy.App;

public static class AppServiceCollection
{
    public static IServiceCollection AddAppServices(this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly); });
        services.AddLitackaApiClientServices(configuration);

        return services;
    }
}
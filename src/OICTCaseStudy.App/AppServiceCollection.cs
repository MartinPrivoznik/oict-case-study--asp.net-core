using Microsoft.Extensions.DependencyInjection;

namespace OICTCaseStudy.App;

public static class AppServiceCollection
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly); });
        return services;
    }
}
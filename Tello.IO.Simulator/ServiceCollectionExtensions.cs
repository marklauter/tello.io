using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Tello.IO.Simulator;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTelloClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.TryAddTransient<ITelloClient, TelloClient>();
        services.TryAddSingleton(configuration.GetSection(nameof(TelloClientOptions)).Get<TelloClientOptions>()
            ?? throw new KeyNotFoundException(nameof(TelloClientOptions)));

        return services;
    }
}

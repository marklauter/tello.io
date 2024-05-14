using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tello.IO.Client;

namespace Tello.IO.Simulator;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSimulatedClientHandler(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.TryAddSingleton<ITelloClientHandler, SimulatedTelloClientHandler>();
        services.TryAddSingleton(configuration.GetSection(nameof(TelloClientHandlerOptions)).Get<TelloClientHandlerOptions>()
            ?? throw new KeyNotFoundException(nameof(TelloClientHandlerOptions)));

        return services;
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Tello.IO.Client;
using Tello.IO.Telemetry;

namespace Tello.IO;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTelloTelemetryService(
        this IServiceCollection services) => services
            .AddLogging()
            .AddHostedService<TelloTelemetryService>();

    public static IServiceCollection AddTelloTelemetryService(
        this IServiceCollection services,
        Action<ILoggingBuilder> configure) => services
            .AddLogging(configure)
            .AddHostedService<TelloTelemetryService>();

    public static IServiceCollection AddTelloClient(
        this IServiceCollection services)
    {
        services.TryAddSingleton<ITelloClient, TelloClient>();

        return services;
    }

    public static IServiceCollection AddDefaultClientHandler(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.TryAddSingleton<ITelloClientHandler, UdpTelloClientHandler>();
        services.TryAddSingleton(configuration
            .GetSection(nameof(TelloClientHandlerOptions))
            .Get<TelloClientHandlerOptions>()
            ?? throw new KeyNotFoundException(nameof(TelloClientHandlerOptions)));

        return services;
    }
}

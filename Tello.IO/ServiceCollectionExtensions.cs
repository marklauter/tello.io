using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tello.IO.Messaging;

namespace Tello.IO;

public static class ServiceCollectionExtensions
{
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
        services.TryAddSingleton(configuration.GetSection(nameof(MessageHandlerOptions)).Get<MessageHandlerOptions>()
            ?? throw new KeyNotFoundException(nameof(MessageHandlerOptions)));

        return services;
    }
}

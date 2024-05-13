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

    public static IServiceCollection AddDefaultMessenger(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.TryAddSingleton<IMessageHandler, UdpMessageHandler>();
        services.TryAddSingleton(configuration.GetSection(nameof(MessageHandlerOptions)).Get<MessageHandlerOptions>()
            ?? throw new KeyNotFoundException(nameof(MessageHandlerOptions)));

        return services;
    }
}

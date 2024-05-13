﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tello.IO.Messaging;

namespace Tello.IO.Simulator;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSimulatedMessageHandler(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.TryAddSingleton<IMessageHandler, SimulatedMessageHandler>();
        services.TryAddSingleton(configuration.GetSection(nameof(MessageHandlerOptions)).Get<MessageHandlerOptions>()
            ?? throw new KeyNotFoundException(nameof(MessageHandlerOptions)));

        return services;
    }
}

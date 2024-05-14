using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Tello.IO.Simulator.Tests;

[ExcludeFromCodeCoverage]
internal sealed class Startup
{
    private readonly IConfiguration configuration;

    public Startup() => configuration = new ConfigurationBuilder()
        .AddInMemoryCollection(new Dictionary<string, string>
        {
            { "MessageHandlerOptions:IPAddress", "192.168.10.1" },
            { "MessageHandlerOptions:Port", "8889" },
        }!)
        .Build();

    public void ConfigureServices(IServiceCollection services) => _ = services
        .AddSimulatedClientHandler(configuration)
        .AddTelloClient();
}


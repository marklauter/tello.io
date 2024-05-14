using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Tello.IO.Tests;

[ExcludeFromCodeCoverage]
internal sealed class Startup
{
    private readonly IConfiguration configuration;

    public Startup() => configuration = new ConfigurationBuilder()
        .AddInMemoryCollection(new Dictionary<string, string>
        {
            { "TelloClientHandlerOptions:IPAddress", "192.168.10.1" },
            { "TelloClientHandlerOptions:Port", "8889" },
        }!)
        .Build();

    public void ConfigureServices(IServiceCollection services) => _ = services
        .AddDefaultClientHandler(configuration)
        .AddTelloClient();
}

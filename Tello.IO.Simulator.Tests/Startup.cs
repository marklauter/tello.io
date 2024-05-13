using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Tello.IO.Simulator.Tests;

[ExcludeFromCodeCoverage]
internal class Startup
{
    private readonly IConfiguration configuration;

    public Startup()
    {
        configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "TelloClientOptions:Port", "8889" },
                { "TelloClientOptions:IPAddress", "192.168.10.1" },
                { "TelloClientOptions:AddressFamily", "2" }, //AddressFamily.InterNetwork
            }!)
            .Build();
    }

    public void ConfigureServices(IServiceCollection services) => _ = services.AddTelloClient(configuration);
}

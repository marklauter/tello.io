using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Tello.IO.Parser.Tests;

[ExcludeFromCodeCoverage]
internal sealed class Startup
{
    public void ConfigureServices(IServiceCollection services) => _ = services
        .AddTelloParser();
}

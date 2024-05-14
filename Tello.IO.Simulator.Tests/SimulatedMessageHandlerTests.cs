using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Tello.IO.Simulator.Tests;

[ExcludeFromCodeCoverage]
public class SimulatedMessageHandlerTests(ITelloClientHandler handler)
{
    [Fact]
    public void InjectionSucceeds() => Assert.NotNull(handler);

    [Fact]
    public async Task SendAsync_Sends_All_Bytes()
    {
        var message = "command";
        var bytesSent = await handler.SendAsync(message, CancellationToken.None);
        Assert.Equal(message.Length, bytesSent);
    }

    [Theory]
    [InlineData("command", "ok")]
    public async Task SendAsync_Sets_Available(string message, string expected)
    {
        _ = await handler.SendAsync(message, CancellationToken.None);
        Assert.Equal(expected.Length, handler.Available);
    }

    [Theory]
    [InlineData("command", "ok")]
    public async Task ReceiveAsync_Returns_Expected_Response(string message, string expected)
    {
        _ = await handler.SendAsync(message, CancellationToken.None);
        Assert.Equal(expected.Length, handler.Available);
        var result = await handler.ReceiveAsync(CancellationToken.None);
        var response = Encoding.UTF8.GetString(result.Buffer);
        Assert.Equal(expected, response);
    }
}

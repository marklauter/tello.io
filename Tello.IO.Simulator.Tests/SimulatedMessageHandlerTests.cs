using System.Diagnostics.CodeAnalysis;
using System.Text;
using Tello.IO.Messaging;

namespace Tello.IO.Simulator.Tests;

[ExcludeFromCodeCoverage]
public class SimulatedMessageHandlerTests(ITelloClientHandler messageHandler)
{
    [Fact]
    public void InjectionSucceeds() => Assert.NotNull(messageHandler);

    [Fact]
    public async Task SendAsync_Sends_All_Bytes()
    {
        var message = "command";
        var bytesSent = await messageHandler.SendAsync(message, CancellationToken.None);
        Assert.Equal(message.Length, bytesSent);
    }

    [Theory]
    [InlineData("command", "ok")]
    public async Task SendAsync_Sets_Available(string message, string expected)
    {
        _ = await messageHandler.SendAsync(message, CancellationToken.None);
        Assert.Equal(expected.Length, messageHandler.Available);
    }

    [Theory]
    [InlineData("command", "ok")]
    public async Task ReceiveAsync_Returns_Expected_Response(string message, string expected)
    {
        _ = await messageHandler.SendAsync(message, CancellationToken.None);
        Assert.Equal(expected.Length, messageHandler.Available);
        var result = await messageHandler.ReceiveAsync(CancellationToken.None);
        var response = Encoding.UTF8.GetString(result.Buffer);
        Assert.Equal(expected, response);
    }
}

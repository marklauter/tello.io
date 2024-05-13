using System.Diagnostics.CodeAnalysis;
using Tello.IO.Messaging;

namespace Tello.IO.Tests;

[ExcludeFromCodeCoverage]
public class UpdMessageHandlerTests(IMessageHandler messageHandler)
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

    //[Fact]
    //public async Task ReceiveAsyncTest()
    //{
    //    var result = await messenger.ReceiveAsync(CancellationToken.None);
    //    Assert.True(result.Buffer.Length > 0);
    //}
}

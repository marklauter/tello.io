using System.Diagnostics.CodeAnalysis;

namespace Tello.IO.Tests;

[ExcludeFromCodeCoverage]
public class UpdMessageHandlerTests(ITelloClientHandler handler)
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

    //[Fact]
    //public async Task ReceiveAsyncTest()
    //{
    //    var result = await messenger.ReceiveAsync(CancellationToken.None);
    //    Assert.True(result.Buffer.Length > 0);
    //}
}

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Tello.IO.Messaging;

internal sealed class UdpMessageHandler(MessageHandlerOptions options)
    : IMessageHandler
    , IDisposable
{
    private readonly UdpClient udpClient = new();
    private readonly IPEndPoint remoteEndPoint = new(IPAddress.Parse(options?.IPAddress ?? throw new ArgumentNullException(nameof(options))), options.Port);

    public int Available => udpClient.Available;

    public async ValueTask<ReceiveResult> ReceiveAsync(CancellationToken cancellationToken)
    {
        var result = await udpClient.ReceiveAsync(cancellationToken);
        return new ReceiveResult(result.Buffer, result.RemoteEndPoint);
    }

    public ValueTask<int> SendAsync(string message, CancellationToken cancellationToken) => udpClient.SendAsync(new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(message)), remoteEndPoint, cancellationToken);

    public void Dispose() => udpClient.Dispose();
}

using System.Net;
using System.Net.Sockets;

namespace Tello.IO.Client;

internal sealed class UdpTelloClientHandler(TelloClientHandlerOptions options)
    : ITelloClientHandler
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

    public ValueTask<int> SendAsync(TelloCommand command, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(command);
        return udpClient.SendAsync(command.AsReadOnlyMemory(), remoteEndPoint, cancellationToken);
    }

    public void Dispose() => udpClient.Dispose();
}

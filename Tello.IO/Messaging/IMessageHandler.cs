namespace Tello.IO.Messaging;

/// <summary>
/// IMessageHandler abstracts the core behavior of UdpClient. This allows us to add custom handling for messages, similar to the behavior of HttpClientHandler.
/// </summary>
public interface IMessageHandler
{
    int Available { get; }
    ValueTask<ReceiveResult> ReceiveAsync(CancellationToken cancellationToken);
    ValueTask<int> SendAsync(string message, CancellationToken cancellationToken);
}

using Tello.IO.Client;

namespace Tello.IO;

/// <summary>
/// ITelloClientHandler abstracts the core behavior of UdpClient. This allows us to add custom handling for commands, similar to the behavior of HttpClientHandler.
/// </summary>
public interface ITelloClientHandler
{
    int Available { get; }
    ValueTask<ReceiveResult> ReceiveAsync(CancellationToken cancellationToken);
    ValueTask<int> SendAsync(TelloCommand command, CancellationToken cancellationToken);
}

using Tello.IO.Messaging;

namespace Tello.IO;

public interface ITelloClient
{
    Task<string> SendAsync(TelloMessage message, CancellationToken cancellationToken);
}

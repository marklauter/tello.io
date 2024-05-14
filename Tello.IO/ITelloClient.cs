using Tello.IO.Client;

namespace Tello.IO;

public interface ITelloClient
{
    Task<string> SendAsync(TelloCommand command, CancellationToken cancellationToken);
}

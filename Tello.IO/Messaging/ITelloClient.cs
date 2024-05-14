namespace Tello.IO.Messaging;

public interface ITelloClient
{
    Task<string> SendAsync(TelloMessage message, CancellationToken cancellationToken);
}

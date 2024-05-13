namespace Tello.IO.Messaging;

public interface ITelloClient
{
    Task<string> SendAsync(string message, CancellationToken cancellationToken);
}

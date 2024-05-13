namespace Tello.IO;

public interface ITelloClient
{
    Task<string> SendAsync(string command);
}

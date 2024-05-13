namespace Tello.IO.Simulator;

internal sealed class TelloClient(TelloClientOptions options)
    : ITelloClient
{
    private readonly TelloClientOptions options = options ?? throw new ArgumentNullException(nameof(options));

    public Task<string> SendAsync(string command) => throw new NotImplementedException();
}

using System.Net;
using System.Text;
using Tello.IO.Messaging;

namespace Tello.IO.Simulator;

internal sealed class SimulatedMessageHandler(MessageHandlerOptions options)
    : IMessageHandler
{
    private readonly Dictionary<string, string> requestResponse = new()
    {
        { "command", "ok" }
    };

    private readonly IPEndPoint remoteEndPoint = new(IPAddress.Parse(options?.IPAddress ?? throw new ArgumentNullException(nameof(options))), options.Port);

    private string response = String.Empty;

    public int Available => response.Length;

    public ValueTask<ReceiveResult> ReceiveAsync(CancellationToken cancellationToken)
    {
        var buffer = Encoding.UTF8.GetBytes(response);
        response = String.Empty;
        return ValueTask.FromResult(new ReceiveResult(buffer, remoteEndPoint));
    }

    public ValueTask<int> SendAsync(string message, CancellationToken cancellationToken)
    {
        this.response = String.Empty;
        if (requestResponse.TryGetValue(message, out var response))
        {
            this.response = response;
        }

        return new ValueTask<int>(Task.FromResult(message.Length));
    }
}

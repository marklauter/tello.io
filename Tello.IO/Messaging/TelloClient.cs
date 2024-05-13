using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Text;

namespace Tello.IO.Messaging;

internal sealed class TelloClient(IMessageHandler messageHandler)
    : ITelloClient
{
    private sealed class Request
    {
        private readonly TaskCompletionSource<string> taskCompletionSource = new();

        public Request(string message, CancellationToken cancellationToken)
        {
            _ = cancellationToken.Register(TrySetCanceled);
            CancellationToken = cancellationToken;
            Message = message ?? String.Empty;
        }

        public CancellationToken CancellationToken { get; }
        public Task<string> Task => taskCompletionSource.Task;

        public string Message { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TrySetResult(string message) => taskCompletionSource.TrySetResult(message);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void TrySetException(Exception ex) => taskCompletionSource.TrySetException(ex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void TrySetCanceled() => taskCompletionSource.TrySetCanceled();
    }

    private readonly IMessageHandler messageHandler = messageHandler ?? throw new ArgumentNullException(nameof(messageHandler));
    private readonly ConcurrentQueue<Request> requests = new();
    private bool processingQueue;

    public async Task<string> SendAsync(string message, CancellationToken cancellationToken)
    {
        var request = new Request(message, cancellationToken);
        requests.Enqueue(request);

        ProcessRequestQueue();

        return await request.Task;
    }

    private void ProcessRequestQueue() => Task.Run(async () =>
    {
        if (processingQueue)
        {
            return;
        }

        processingQueue = true;
        try
        {
            while (requests.TryDequeue(out var request))
            {
                await SendMessageAsync(request);
            }
        }
        finally
        {
            processingQueue = false;
        }
    });

    private async Task SendMessageAsync(Request request)
    {
        try
        {
            var bytesSent = await messageHandler.SendAsync(request.Message, request.CancellationToken);
            if (request.Message.Length != bytesSent)
            {
                throw new InvalidOperationException($"Failed to send message. bytes sent: {bytesSent}, expected: {request.Message.Length}");
            }

            while (messageHandler.Available == 0)
            {
                await Task.Yield();
            }

            var result = await messageHandler.ReceiveAsync(request.CancellationToken);
            var response = Encoding.ASCII.GetString(result.Buffer);

            if (String.IsNullOrEmpty(response))
            {
                throw new InvalidDataException("Received empty response.");
            }

            _ = request.TrySetResult(response);
        }
        catch (Exception ex)
        {
            request.TrySetException(ex);
        }
    }
}

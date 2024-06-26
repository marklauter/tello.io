﻿using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Text;

namespace Tello.IO.Client;

internal sealed class TelloClient(ITelloClientHandler handler)
    : ITelloClient
{
    private sealed class Request
    {
        private readonly TaskCompletionSource<string> taskCompletionSource = new();

        public Request(TelloCommand command, CancellationToken cancellationToken)
        {
            _ = cancellationToken.Register(TrySetCanceled);
            CancellationToken = cancellationToken;
            Command = command;
        }

        public CancellationToken CancellationToken { get; }
        public Task<string> Task => taskCompletionSource.Task;

        public TelloCommand Command { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TrySetResult(string response) => taskCompletionSource.TrySetResult(response);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void TrySetException(Exception ex) => taskCompletionSource.TrySetException(ex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void TrySetCanceled() => taskCompletionSource.TrySetCanceled();
    }

    private readonly ITelloClientHandler handler = handler ?? throw new ArgumentNullException(nameof(handler));
    private readonly ConcurrentQueue<Request> requests = new();
    private bool processingQueue;

    public Task<string> SendAsync(TelloCommand command, CancellationToken cancellationToken) =>
        InvokeRequest(new Request(command, cancellationToken)).Task;

    private Request InvokeRequest(Request request)
    {
        requests.Enqueue(request);
        _ = Task.Factory.StartNew(InvokeRequestsAsync);
        return request;
    }

    private async Task InvokeRequestsAsync()
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
                if (!request.CancellationToken.IsCancellationRequested)
                {
                    await SendRequestAsync(request);
                }
            }
        }
        finally
        {
            processingQueue = false;
        }
    }

    private async Task SendRequestAsync(Request request)
    {
        try
        {
            var bytesSent = await handler.SendAsync(request.Command, request.CancellationToken);
            if (request.Command.Length != bytesSent)
            {
                throw new InvalidOperationException($"Failed to send command. bytes sent: {bytesSent}, expected: {request.Command.Length}");
            }

            if (IgnoreResponse(request))
            {
                return;
            }

            while (handler.Available == 0)
            {
                await Task.Yield();
            }

            var result = await handler.ReceiveAsync(request.CancellationToken);
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

    // todo: better way to do this is regex matching
    private static bool IgnoreResponse(Request request) => request.Command.ToKey() is "reboot" or "rc {0} {1} {2} {3}";
}

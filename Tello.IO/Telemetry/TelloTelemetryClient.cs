using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;
using System.Text;

namespace Tello.IO.Telemetry;

internal sealed class TelloTelemetryService(
    TelemetryClientOptions options,
    ILogger<TelloTelemetryService> logger)
    : BackgroundService
    , IDisposable
{
    private readonly UdpClient client = new(options?.Port ?? throw new ArgumentNullException(nameof(options)));
    private readonly ILogger<TelloTelemetryService> logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private bool disposed;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("telemetry client started {Timestamp}", DateTime.UtcNow);

        while (!stoppingToken.IsCancellationRequested)
        {
            if (client.Available > 0)
            {
                var result = await client.ReceiveAsync(stoppingToken);
                var data = Encoding.UTF8.GetString(result.Buffer);
                // todo: write a custom Serilog sink for the telemetry
                logger.LogInformation("{@Telemetry}", new TelloTelemetry(DateTime.UtcNow, data));
            }

            await Task.Yield();
        }

        logger.LogInformation("telemetry client stopped {Timestamp}", DateTime.UtcNow);
    }

    private void Dispose(bool disposing)
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        if (disposing)
        {
            client.Dispose();
        }
    }

    public override void Dispose()
    {
        base.Dispose();
        Dispose(disposing: true);
    }
}

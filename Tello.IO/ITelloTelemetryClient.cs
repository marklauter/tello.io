using Tello.IO.Telemetry;

namespace Tello.IO;

public interface ITelloTelemetryClient
{
    Task ListenAsync(Action<TelloTelemetry> telemetryReceived, CancellationToken cancellationToken);
}

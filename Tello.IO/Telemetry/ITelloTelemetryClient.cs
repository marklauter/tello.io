namespace Tello.IO.Telemetry;

public interface ITelloTelemetryClient
{
    Task ListenAsync(Action<TelloTelemetry> telemetryReceived, CancellationToken cancellationToken);
}

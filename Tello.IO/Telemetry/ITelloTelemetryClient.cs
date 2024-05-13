namespace Tello.IO.Telemetry;

public interface ITelloTelemetryClient
{
    void Listen(CancellationToken cancellationToken, Action<TelloTelemetry> telemetryReceived);
}

namespace Tello.IO.Telemetry;

public record TelemetryClientOptions(string IPAddress, int Port)
    : UdpClientOptions(IPAddress, Port);

namespace Tello.IO.Messaging;

public record MessageHandlerOptions(string IPAddress, int Port)
    : UdpClientOptions(IPAddress, Port);

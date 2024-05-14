namespace Tello.IO.Client;

public record TelloClientHandlerOptions(string IPAddress, int Port)
    : UdpClientOptions(IPAddress, Port);

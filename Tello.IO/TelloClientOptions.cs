using System.Net.Sockets;

namespace Tello.IO;

public record TelloClientOptions(
    int Port,
    string IPAddress,
    AddressFamily AddressFamily = AddressFamily.InterNetwork);

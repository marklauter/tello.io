using System.Net;

namespace Tello.IO.Client;

public readonly record struct ReceiveResult(byte[] Buffer, IPEndPoint RemoteEndPoint);

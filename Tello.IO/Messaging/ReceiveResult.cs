using System.Net;

namespace Tello.IO.Messaging;

public readonly record struct ReceiveResult(byte[] Buffer, IPEndPoint RemoteEndPoint);

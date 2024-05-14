using System.Net;
using System.Text;
using Tello.IO.Client;

namespace Tello.IO.Simulator;

internal sealed class SimulatedTelloClientHandler(TelloClientHandlerOptions options)
    : ITelloClientHandler
{
    private sealed class Drone
    {
        private readonly DateTime start = DateTime.UtcNow;

        public int Speed { get; set; } = 10;
        public int Battery { get; set; } = 99;
        public int Time => Convert.ToInt32((DateTime.UtcNow - start).TotalSeconds);
        public int Wifi { get; set; } = 1;
        public string Sdk { get; set; } = "03";
        public string Sn { get; set; } = "SN123456789012345";
        public string Hardware { get; set; } = "TELLO";
        public string WifiVersion { get; set; } = "wifiv1.0.0.0";
        public string Ap { get; set; } = "not supported";
        public string Ssid { get; set; } = "SSID UN/PW";
    }

    private readonly Dictionary<string, string> requestResponse = new()
    {
        // control commands
        { "reboot", String.Empty },
        { "command", "ok" },
        { "takeoff", "ok" },
        { "land", "ok" },
        { "stop", "ok" },
        { "streamon", "ok" },
        { "streamoff", "ok" },
        { "emergency", "ok" },
        { "up {0}", "ok" },
        { "down {0}", "ok" },
        { "left {0}", "ok" },
        { "right {0}", "ok" },
        { "forward {0}", "ok" },
        { "back {0}", "ok" },
        { "cw {0}", "ok" },
        { "ccw {0}", "ok" },
        { "motoron", "ok" },
        { "motoroff", "ok" },
        { "throwfly", "ok" },
        { "flip {0}", "ok" },
        { "go {0} {1} {2} {3}", "ok" },
        { "curve {0} {1} {2} {3} {4} {5} {6}", "ok" },

        // configuration commands
        { "speed {cm}", "ok" },
        { "rc {0} {1} {2} {3}", String.Empty },
        { "wifi {0} {1}", "OK, drone will reboot in 3s" },
        { "ap {0} {1}","OK, drone will reboot in 3s" },
        { "wifisetchannel {channel}", "ok" },
        { "port {0} {1}", "ok" },
        { "setfps {0}", "ok" },
        { "setbitrate {0}", "ok" },
        { "setresolution {0}", "ok" },

        // queries
        { "speed?", "10" },
        { "battery?", "99" },
        { "time?", "60" },
        { "wifi?", "1" },
        { "sdk?", "03" },
        { "sn?", "SN123456789012345" },
        { "hardware?", "TELLO" },
        { "wifiversion?", "wifiv1.0.0.0" },
        { "ap?", "not supported" },
        { "ssid?", "ssid" },
    };

    private readonly IPEndPoint remoteEndPoint = new(IPAddress.Parse(options?.IPAddress ?? throw new ArgumentNullException(nameof(options))), options.Port);

    private string response = String.Empty;

    public int Available => response.Length;

    public ValueTask<ReceiveResult> ReceiveAsync(CancellationToken cancellationToken)
    {
        var buffer = Encoding.UTF8.GetBytes(response);
        response = String.Empty;
        return ValueTask.FromResult(new ReceiveResult(buffer, remoteEndPoint));
    }

    public ValueTask<int> SendAsync(TelloCommand command, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(command);

        this.response = String.Empty;
        if (requestResponse.TryGetValue(command.ToKey(), out var response))
        {
            this.response = response;
        }

        return new ValueTask<int>(Task.FromResult(command.Length));
    }
}

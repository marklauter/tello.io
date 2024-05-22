using Lexi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Tello.IO.Parser;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTelloParser(this IServiceCollection services)
    {
        var lexer = VocabularyBuilder
            .Create()
            .Ignore(CommonPatterns.Whitespace(), (uint)TokenId.WhiteSpace)
            .Match("reboot", (uint)TokenId.Reboot)
            .Match("command", (uint)TokenId.Command)
            .Match("takeoff", (uint)TokenId.Takeoff)
            .Match("land", (uint)TokenId.Land)
            .Match("stop", (uint)TokenId.Stop)
            .Match("streamon", (uint)TokenId.StreamOn)
            .Match("streamoff", (uint)TokenId.StreamOff)
            .Match("emergency", (uint)TokenId.Emergency)
            .Match("up", (uint)TokenId.MoveUp)
            .Match("down", (uint)TokenId.MoveDown)
            .Match("left", (uint)TokenId.MoveLeft)
            .Match("right", (uint)TokenId.MoveRight)
            .Match("forward", (uint)TokenId.MoveForward)
            .Match("back", (uint)TokenId.MoveBack)
            .Match("cw", (uint)TokenId.RotateClockwise)
            .Match("ccw", (uint)TokenId.RotateCounterClockwise)
            .Match("motoron", (uint)TokenId.MotorOn)
            .Match("motoroff", (uint)TokenId.MotorOff)
            .Match("throwfly", (uint)TokenId.ThrowFly)
            .Match("flip", (uint)TokenId.Flip)
            .Match("go", (uint)TokenId.MoveToPosition)
            .Match("curve", (uint)TokenId.Curve)
            .Match("speed", (uint)TokenId.WriteSpeed)
            .Match("rc", (uint)TokenId.WriteRemoteControl)
            .Match("wifi", (uint)TokenId.WriteWifi)
            .Match("ap", (uint)TokenId.WriteAccessPoint)
            .Match("wifisetchannel", (uint)TokenId.WriteWifiChannel)
            .Match("port", (uint)TokenId.WritePort)
            .Match("setfps", (uint)TokenId.WriteFramesPerSecond)
            .Match("setbitrate", (uint)TokenId.WriteBitrate)
            .Match("setresolution", (uint)TokenId.WriteResolution)
            .Match(@"speed\?", (uint)TokenId.ReadSpeed)
            .Match(@"battery\?", (uint)TokenId.ReadBattery)
            .Match(@"time\?", (uint)TokenId.ReadTime)
            .Match(@"wifi\?", (uint)TokenId.ReadWifiVersion)
            .Match(@"sdk\?", (uint)TokenId.ReadSdkVersion)
            .Match(@"sn\?", (uint)TokenId.ReadSerialNumber)
            .Match(@"hardware\?", (uint)TokenId.ReadHardware)
            .Match(@"ap\?", (uint)TokenId.ReadAccessPoint)
            .Match(@"ssid\?", (uint)TokenId.ReadSsid)
            .Match("l", (uint)TokenId.DirectionLeft)
            .Match("r", (uint)TokenId.DirectionRight)
            .Match("f", (uint)TokenId.DirectionFront)
            .Match("b", (uint)TokenId.DirectionBack)
            .Match(CommonPatterns.IntegerLiteral(), (uint)TokenId.IntegerLiteral)
            .Match(CommonPatterns.Identifier(), (uint)TokenId.Identifier)
            .Build();

        services.TryAddSingleton(lexer);

        return services;
    }
}

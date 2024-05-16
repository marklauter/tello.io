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
            .Match("l", TokenKind.DirectionLeft)
            .Match("r", TokenKind.DirectionRight)
            .Match("f", TokenKind.DirectionFront)
            .Match("b", TokenKind.DirectionBack)
            .Match("reboot", TokenKind.Reboot)
            .Match("command", TokenKind.Command)
            .Match("takeoff", TokenKind.Takeoff)
            .Match("land", TokenKind.Land)
            .Match("stop", TokenKind.Stop)
            .Match("streamon", TokenKind.StreamOn)
            .Match("streamoff", TokenKind.StreamOff)
            .Match("emergency", TokenKind.Emergency)
            .Match("up", TokenKind.MoveUp)
            .Match("down", TokenKind.MoveDown)
            .Match("left", TokenKind.MoveLeft)
            .Match("right", TokenKind.MoveRight)
            .Match("forward", TokenKind.MoveForward)
            .Match("back", TokenKind.MoveBack)
            .Match("cw", TokenKind.RotateClockwise)
            .Match("ccw", TokenKind.RotateCounterClockwise)
            .Match("motoron", TokenKind.MotorOn)
            .Match("motoroff", TokenKind.MotorOff)
            .Match("throwfly", TokenKind.ThrowFly)
            .Match("flip", TokenKind.Flip)
            .Match("go", TokenKind.MoveToPosition)
            .Match("curve", TokenKind.Curve)
            .Match("speed", TokenKind.WriteSpeed)
            .Match("rc", TokenKind.WriteRemoteControl)
            .Match("wifi", TokenKind.WriteWifi)
            .Match("ap", TokenKind.WriteAccessPoint)
            .Match("wifisetchannel", TokenKind.WriteWifiChannel)
            .Match("port", TokenKind.WritePort)
            .Match("setfps", TokenKind.WriteFramesPerSecond)
            .Match("setbitrate", TokenKind.WriteBitrate)
            .Match("setresolution", TokenKind.WriteResolution)
            .Match(@"speed\?", TokenKind.ReadSpeed)
            .Match(@"battery\?", TokenKind.ReadBattery)
            .Match(@"time\?", TokenKind.ReadTime)
            .Match(@"wifi\?", TokenKind.ReadWifiVersion)
            .Match(@"sdk\?", TokenKind.ReadSdkVersion)
            .Match(@"sn\?", TokenKind.ReadSerialNumber)
            .Match(@"hardware\?", TokenKind.ReadHardware)
            .Match(@"ap\?", TokenKind.ReadAccessPoint)
            .Match(@"ssid\?", TokenKind.ReadSsid)
            .Match(CommonPatterns.IntegerLiteral(), TokenKind.IntegerLiteral)
            .Match(CommonPatterns.Identifier(), TokenKind.Identifier)
            .Build();

        services.TryAddSingleton(lexer);

        return services;
    }
}

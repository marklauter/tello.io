using Lexi;
using System.Diagnostics.CodeAnalysis;

namespace Tello.IO.Parser.Tests;

[ExcludeFromCodeCoverage]
public class LexerTests(Lexer lexer)
{
    private readonly Lexer lexer = lexer ?? throw new ArgumentNullException(nameof(lexer));

    [Fact]
    public void DependencyInjectionWorks() => Assert.NotNull(lexer);

    [Theory]
    [InlineData("reboot", TokenId.Reboot)]
    [InlineData("command", TokenId.Command)]
    [InlineData("takeoff", TokenId.Takeoff)]
    [InlineData("land", TokenId.Land)]
    [InlineData("stop", TokenId.Stop)]
    [InlineData("streamon", TokenId.StreamOn)]
    [InlineData("streamoff", TokenId.StreamOff)]
    [InlineData("emergency", TokenId.Emergency)]
    [InlineData("up", TokenId.MoveUp)]
    [InlineData("down", TokenId.MoveDown)]
    [InlineData("left", TokenId.MoveLeft)]
    [InlineData("right", TokenId.MoveRight)]
    [InlineData("forward", TokenId.MoveForward)]
    [InlineData("back", TokenId.MoveBack)]
    [InlineData("cw", TokenId.RotateClockwise)]
    [InlineData("ccw", TokenId.RotateCounterClockwise)]
    [InlineData("motoron", TokenId.MotorOn)]
    [InlineData("motoroff", TokenId.MotorOff)]
    [InlineData("throwfly", TokenId.ThrowFly)]
    [InlineData("flip", TokenId.Flip)]
    [InlineData("go", TokenId.MoveToPosition)]
    [InlineData("curve", TokenId.Curve)]
    [InlineData("speed", TokenId.WriteSpeed)]
    [InlineData("rc", TokenId.WriteRemoteControl)]
    [InlineData("wifi", TokenId.WriteWifi)]
    [InlineData("ap", TokenId.WriteAccessPoint)]
    [InlineData("wifisetchannel", TokenId.WriteWifiChannel)]
    [InlineData("port", TokenId.WritePort)]
    [InlineData("setfps", TokenId.WriteFramesPerSecond)]
    [InlineData("setbitrate", TokenId.WriteBitrate)]
    [InlineData("setresolution", TokenId.WriteResolution)]
    [InlineData("speed?", TokenId.ReadSpeed)]
    [InlineData("battery?", TokenId.ReadBattery)]
    [InlineData("time?", TokenId.ReadTime)]
    [InlineData("wifi?", TokenId.ReadWifiVersion)]
    [InlineData("sdk?", TokenId.ReadSdkVersion)]
    [InlineData("sn?", TokenId.ReadSerialNumber)]
    [InlineData("hardware?", TokenId.ReadHardware)]
    [InlineData("ap?", TokenId.ReadAccessPoint)]
    [InlineData("ssid?", TokenId.ReadSsid)]
    public void NextMatch_Returns_Expected_Token(string input, TokenId expected)
    {
        var result = lexer.NextMatch(input);
        Assert.True(result.Symbol.IsMatch);
        Assert.True(result.Source.IsEndOfSource);
        Assert.Equal((uint)expected, result.Symbol.TokenId);
    }

    [Theory]
    [InlineData("up 100", TokenId.MoveUp, TokenId.IntegerLiteral)]
    [InlineData("down 50", TokenId.MoveDown, TokenId.IntegerLiteral)]
    [InlineData("left 60", TokenId.MoveLeft, TokenId.IntegerLiteral)]
    [InlineData("right 60", TokenId.MoveRight, TokenId.IntegerLiteral)]
    [InlineData("forward 60", TokenId.MoveForward, TokenId.IntegerLiteral)]
    [InlineData("back 60", TokenId.MoveBack, TokenId.IntegerLiteral)]
    [InlineData("cw 180", TokenId.RotateClockwise, TokenId.IntegerLiteral)]
    [InlineData("ccw 360", TokenId.RotateCounterClockwise, TokenId.IntegerLiteral)]
    [InlineData("flip l", TokenId.Flip, TokenId.DirectionLeft)]
    [InlineData("flip r", TokenId.Flip, TokenId.DirectionRight)]
    [InlineData("flip b", TokenId.Flip, TokenId.DirectionBack)]
    [InlineData("flip f", TokenId.Flip, TokenId.DirectionFront)]
    [InlineData("go 0 50 50 25", TokenId.MoveToPosition, TokenId.IntegerLiteral, TokenId.IntegerLiteral, TokenId.IntegerLiteral, TokenId.IntegerLiteral)]
    [InlineData("curve 0 50 50 0 100 100 25", TokenId.Curve, TokenId.IntegerLiteral, TokenId.IntegerLiteral, TokenId.IntegerLiteral, TokenId.IntegerLiteral, TokenId.IntegerLiteral, TokenId.IntegerLiteral, TokenId.IntegerLiteral)]
    [InlineData("speed 20", TokenId.WriteSpeed, TokenId.IntegerLiteral)]
    [InlineData("rc 0 50 -20 -9", TokenId.WriteRemoteControl, TokenId.IntegerLiteral, TokenId.IntegerLiteral, TokenId.IntegerLiteral, TokenId.IntegerLiteral)]
    [InlineData("wifi ssidvalue password", TokenId.WriteWifi, TokenId.Identifier, TokenId.Identifier)]
    [InlineData("ap ssidvalue password", TokenId.WriteAccessPoint, TokenId.Identifier, TokenId.Identifier)]
    [InlineData("wifisetchannel 10", TokenId.WriteWifiChannel, TokenId.IntegerLiteral)]
    [InlineData("port 8889 8899", TokenId.WritePort, TokenId.IntegerLiteral, TokenId.IntegerLiteral)]
    [InlineData("setfps low", TokenId.WriteFramesPerSecond, TokenId.Identifier)]
    [InlineData("setfps middle", TokenId.WriteFramesPerSecond, TokenId.Identifier)]
    [InlineData("setfps high", TokenId.WriteFramesPerSecond, TokenId.Identifier)]
    [InlineData("setbitrate 5", TokenId.WriteBitrate, TokenId.IntegerLiteral)]
    [InlineData("setresolution low", TokenId.WriteResolution, TokenId.Identifier)]
    [InlineData("setresolution high", TokenId.WriteResolution, TokenId.Identifier)]
    public void NextMatch_Returns_Expected_Tokens(string input, params TokenId[] expected)
    {
        var i = 0;
        var result = lexer.NextMatch(input);
        while (result.Symbol.IsMatch)
        {
            Assert.True(result.Symbol.IsMatch);
            Assert.Equal((uint)expected[i++], result.Symbol.TokenId);
            result = lexer.NextMatch(result);
        }
    }
}

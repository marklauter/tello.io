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
    [InlineData("reboot", TokenKind.Reboot)]
    [InlineData("command", TokenKind.Command)]
    [InlineData("takeoff", TokenKind.Takeoff)]
    [InlineData("land", TokenKind.Land)]
    [InlineData("stop", TokenKind.Stop)]
    [InlineData("streamon", TokenKind.StreamOn)]
    [InlineData("streamoff", TokenKind.StreamOff)]
    [InlineData("emergency", TokenKind.Emergency)]
    [InlineData("up", TokenKind.MoveUp)]
    [InlineData("down", TokenKind.MoveDown)]
    [InlineData("left", TokenKind.MoveLeft)]
    [InlineData("right", TokenKind.MoveRight)]
    [InlineData("forward", TokenKind.MoveForward)]
    [InlineData("back", TokenKind.MoveBack)]
    [InlineData("cw", TokenKind.RotateClockwise)]
    [InlineData("ccw", TokenKind.RotateCounterClockwise)]
    [InlineData("motoron", TokenKind.MotorOn)]
    [InlineData("motoroff", TokenKind.MotorOff)]
    [InlineData("throwfly", TokenKind.ThrowFly)]
    [InlineData("flip", TokenKind.Flip)]
    [InlineData("go", TokenKind.MoveToPosition)]
    [InlineData("curve", TokenKind.Curve)]
    [InlineData("speed", TokenKind.WriteSpeed)]
    [InlineData("rc", TokenKind.WriteRemoteControl)]
    [InlineData("wifi", TokenKind.WriteWifi)]
    [InlineData("ap", TokenKind.WriteAccessPoint)]
    [InlineData("wifisetchannel", TokenKind.WriteWifiChannel)]
    [InlineData("port", TokenKind.WritePort)]
    [InlineData("setfps", TokenKind.WriteFramesPerSecond)]
    [InlineData("setbitrate", TokenKind.WriteBitrate)]
    [InlineData("setresolution", TokenKind.WriteResolution)]
    [InlineData("speed?", TokenKind.ReadSpeed)]
    [InlineData("battery?", TokenKind.ReadBattery)]
    [InlineData("time?", TokenKind.ReadTime)]
    [InlineData("wifi?", TokenKind.ReadWifiVersion)]
    [InlineData("sdk?", TokenKind.ReadSdkVersion)]
    [InlineData("sn?", TokenKind.ReadSerialNumber)]
    [InlineData("hardware?", TokenKind.ReadHardware)]
    [InlineData("ap?", TokenKind.ReadAccessPoint)]
    [InlineData("ssid?", TokenKind.ReadSsid)]
    public void NextMatch_Returns_Expected_Token(string input, TokenKind expected)
    {
        var result = lexer.NextMatch(input);
        Assert.True(result.Symbol.IsMatch);
        Assert.True(result.Source.IsEndOfSource);
        Assert.Equal((int)expected, result.Symbol.TokenId);
    }

    [Theory]
    [InlineData("up 100", new[] { TokenKind.MoveUp, TokenKind.IntegerLiteral })]
    [InlineData("down 50", new[] { TokenKind.MoveDown, TokenKind.IntegerLiteral })]
    [InlineData("left 60", new[] { TokenKind.MoveLeft, TokenKind.IntegerLiteral })]
    [InlineData("right 60", new[] { TokenKind.MoveRight, TokenKind.IntegerLiteral })]
    [InlineData("forward 60", new[] { TokenKind.MoveForward, TokenKind.IntegerLiteral })]
    [InlineData("back 60", new[] { TokenKind.MoveBack, TokenKind.IntegerLiteral })]
    [InlineData("cw 180", new[] { TokenKind.RotateClockwise, TokenKind.IntegerLiteral })]
    [InlineData("ccw 360", new[] { TokenKind.RotateCounterClockwise, TokenKind.IntegerLiteral })]
    [InlineData("flip l", new[] { TokenKind.Flip, TokenKind.DirectionLeft })]
    [InlineData("flip r", new[] { TokenKind.Flip, TokenKind.DirectionRight })]
    [InlineData("flip b", new[] { TokenKind.Flip, TokenKind.DirectionBack })]
    [InlineData("flip f", new[] { TokenKind.Flip, TokenKind.DirectionFront })]
    [InlineData("go 0 50 50 25", new[] { TokenKind.MoveToPosition, TokenKind.IntegerLiteral, TokenKind.IntegerLiteral, TokenKind.IntegerLiteral, TokenKind.IntegerLiteral })]
    [InlineData("curve 0 50 50 0 100 100 25", new[] { TokenKind.Curve, TokenKind.IntegerLiteral, TokenKind.IntegerLiteral, TokenKind.IntegerLiteral, TokenKind.IntegerLiteral, TokenKind.IntegerLiteral, TokenKind.IntegerLiteral, TokenKind.IntegerLiteral })]
    [InlineData("speed 20", new[] { TokenKind.WriteSpeed, TokenKind.IntegerLiteral })]
    [InlineData("rc 0 50 -20 -9", new[] { TokenKind.WriteRemoteControl, TokenKind.IntegerLiteral, TokenKind.IntegerLiteral, TokenKind.IntegerLiteral, TokenKind.IntegerLiteral })]
    [InlineData("wifi ssidvalue password", new[] { TokenKind.WriteWifi, TokenKind.Identifier, TokenKind.Identifier })]
    [InlineData("ap ssidvalue password", new[] { TokenKind.WriteAccessPoint, TokenKind.Identifier, TokenKind.Identifier })]
    [InlineData("wifisetchannel 10", new[] { TokenKind.WriteWifiChannel, TokenKind.IntegerLiteral })]
    [InlineData("port 8889 8899", new[] { TokenKind.WritePort, TokenKind.IntegerLiteral, TokenKind.IntegerLiteral })]
    [InlineData("setfps low", new[] { TokenKind.WriteFramesPerSecond, TokenKind.Identifier })]
    [InlineData("setfps middle", new[] { TokenKind.WriteFramesPerSecond, TokenKind.Identifier })]
    [InlineData("setfps high", new[] { TokenKind.WriteFramesPerSecond, TokenKind.Identifier })]
    [InlineData("setbitrate 5", new[] { TokenKind.WriteBitrate, TokenKind.IntegerLiteral })]
    [InlineData("setresolution low", new[] { TokenKind.WriteResolution, TokenKind.Identifier })]
    [InlineData("setresolution high", new[] { TokenKind.WriteResolution, TokenKind.Identifier })]
    public void NextMatch_Returns_Expected_Tokens(string input, TokenKind[] expectedTokens)
    {
        var i = 0;
        var result = lexer.NextMatch(input);
        while (result.Symbol.IsMatch)
        {
            Assert.True(result.Symbol.IsMatch);
            Assert.Equal((int)expectedTokens[i++], result.Symbol.TokenId);
            result = lexer.NextMatch(result);
        }
    }
}

using System.Diagnostics.CodeAnalysis;

namespace Tello.IO.Tests;

[ExcludeFromCodeCoverage]
public class TelloClientTests(ITelloClient telloClient)
{
    [Fact]
    public void InjectionSucceeds() => Assert.NotNull(telloClient);
}

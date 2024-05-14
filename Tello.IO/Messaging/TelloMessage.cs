using System.Text;

namespace Tello.IO.Messaging;

public readonly struct TelloMessage
{
    public TelloMessage(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        this.value = value.ToLowerInvariant();
    }

    private readonly string value;

    public int Length => value.Length;

    public ReadOnlyMemory<byte> AsReadOnlyMemory() => new(Encoding.UTF8.GetBytes(value));

    // todo: better way to do this is regex matching
    public string ToKey()
    {
        var parts = value.Split(' ');

        var builder = new StringBuilder(parts[0]);
        for (var i = 0; i < parts.Length - 1; i++)
        {
            _ = builder.Append(" {").Append(i).Append('}');
        }

        return builder.ToString();
    }

    public static implicit operator string(TelloMessage command) => command.value;
    public static implicit operator TelloMessage(string message) => new(message);
}

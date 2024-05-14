using System.Text;

namespace Tello.IO.Client;

public readonly struct TelloCommand
{
    public TelloCommand(string value)
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

    public static implicit operator string(TelloCommand command) => command.value;
    public static implicit operator TelloCommand(string value) => new(value);
}

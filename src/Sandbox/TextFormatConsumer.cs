namespace Sandbox;

public sealed class TextFormatConsumer : IConsumer<TextSubmitted>
{
    public string? Last { get; private set; }

    public void Consume(TextSubmitted message)
    {
        ArgumentNullException.ThrowIfNull(message);

        var trimmed = message.Text.TrimStart();
        if (TextTools.StartsWithIgnoreCase(trimmed, "rev:"))
        {
            Last = TextTools.Reverse(trimmed.Substring(4));
        }
        else
        {
            Last = TextTools.Capitalize(message.Text);
        }
    }
}

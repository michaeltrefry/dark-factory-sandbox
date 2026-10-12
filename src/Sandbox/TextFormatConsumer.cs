namespace Sandbox;

public sealed class TextFormatConsumer : IConsumer<TextSubmitted>
{
    public string? Last { get; private set; }

    public void Consume(TextSubmitted message)
    {
        ArgumentNullException.ThrowIfNull(message);

        if (TextTools.StartsWithIgnoreCase(message.Text, "rev:"))
        {
            Last = TextTools.Reverse(message.Text.Substring(4));
        }
        else
        {
            Last = TextTools.Capitalize(message.Text);
        }
    }
}

namespace Sandbox;

public sealed class TextSubmitted
{
    public TextSubmitted(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        Text = text;
    }

    public string Text { get; }
}

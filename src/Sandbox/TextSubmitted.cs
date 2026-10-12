namespace Sandbox;

public sealed class TextSubmitted
{
    public TextSubmitted(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Text cannot be empty or whitespace.", nameof(text));
        Text = text;
    }

    public string Text { get; }
}
// [dark-factory e2e] P3-AT4 blast-radius probe

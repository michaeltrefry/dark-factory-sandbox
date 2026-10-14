namespace Sandbox;

public sealed class TextStatsConsumer : IConsumer<TextSubmitted>
{
    public TextStats? Last { get; private set; }

    public void Consume(TextSubmitted message)
    {
        ArgumentNullException.ThrowIfNull(message);

        Last = new TextStats(
            TextTools.WordCount(message.Text),
            TextTools.CountLines(message.Text),
            CountSpaces(message.Text));
    }

    // Whitespace other than line terminators, which CountLines already accounts for.
    private static int CountSpaces(string text)
    {
        var count = 0;
        foreach (var ch in text)
        {
            if (char.IsWhiteSpace(ch) && ch is not ('\r' or '\n' or (char)0x2028 or (char)0x2029))
            {
                count++;
            }
        }
        return count;
    }
}

public sealed record TextStats(int Words, int Lines, int Spaces);

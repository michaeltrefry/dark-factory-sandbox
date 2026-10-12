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
            TextTools.CountChar(message.Text, ' '));
    }
}

public sealed record TextStats(int Words, int Lines, int Spaces);

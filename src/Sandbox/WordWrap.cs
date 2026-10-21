namespace Sandbox;

public static class WordWrap
{
    /// <summary>
    /// Wraps <paramref name="text"/> at word boundaries into lines no longer than
    /// <paramref name="width"/> joined by "\n"; words longer than <paramref name="width"/> stay
    /// on their own line and runs of whitespace collapse into single spaces.
    /// </summary>
    public static string Wrap(string text, int width)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentOutOfRangeException.ThrowIfLessThan(width, 1);

        var words = text.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
        var lines = new List<string>();
        var current = new System.Text.StringBuilder();
        foreach (var word in words)
        {
            if (current.Length > 0 && current.Length + 1 + word.Length <= width)
            {
                current.Append(' ').Append(word);
            }
            else
            {
                if (current.Length > 0)
                {
                    lines.Add(current.ToString());
                    current.Clear();
                }
                current.Append(word);
            }
        }
        if (current.Length > 0)
        {
            lines.Add(current.ToString());
        }
        return string.Join("\n", lines);
    }
}

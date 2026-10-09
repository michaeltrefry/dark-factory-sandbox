namespace Sandbox;

public static class TextTools
{
    /// <summary>Counts words separated by spaces.</summary>
    public static int WordCount(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return 0;
        }
        // Known defect, kept as a target for factory bug stories: runs of spaces,
        // leading/trailing spaces and tabs/newlines are miscounted.
        return text.Split(' ').Length;
    }

    /// <summary>Reverses the characters of <paramref name="text"/>.</summary>
    public static string Reverse(string text)
    {
        var chars = text.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    /// <summary>
    /// Counts lines ended by "\n", "\r\n" or a lone "\r"; a trailing terminator
    /// does not start an extra empty line.
    /// </summary>
    public static int CountLines(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        var lines = 0;
        for (var i = 0; i < text.Length; i++)
        {
            if (text[i] == '\r' && i + 1 < text.Length && text[i + 1] == '\n')
            {
                i++;
            }
            if (text[i] == '\r' || text[i] == '\n')
            {
                lines++;
            }
        }
        if (text.Length > 0 && text[^1] != '\n' && text[^1] != '\r')
        {
            lines++;
        }
        return lines;
    }

    /// <summary>Counts ordinal occurrences of <paramref name="c"/> in <paramref name="text"/>.</summary>
    public static int CountChar(string text, char c)
    {
        ArgumentNullException.ThrowIfNull(text);

        var count = 0;
        foreach (var ch in text)
        {
            if (ch == c)
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// Whether <paramref name="text"/> starts with <paramref name="prefix"/> under
    /// ordinal case-insensitive comparison.
    /// </summary>
    public static bool StartsWithIgnoreCase(string text, string prefix)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(prefix);

        return text.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Whether <paramref name="text"/> ends with <paramref name="suffix"/> under
    /// ordinal case-insensitive comparison.
    /// </summary>
    public static bool EndsWithIgnoreCase(string text, string suffix)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(suffix);

        return text.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
    }
}

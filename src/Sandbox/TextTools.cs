namespace Sandbox;

public static class TextTools
{
    /// <summary>
    /// Counts words separated by runs of whitespace; whitespace-only text has no words.
    /// </summary>
    public static int WordCount(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return 0;
        }
        return text.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    /// <summary>Reverses the characters of <paramref name="text"/>.</summary>
    public static string Reverse(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        var chars = text.ToCharArray();
        Array.Reverse(chars);
        // Array.Reverse splits surrogate pairs; swap each reversed pair back together.
        for (var i = 0; i < chars.Length - 1; i++)
        {
            if (char.IsLowSurrogate(chars[i]) && char.IsHighSurrogate(chars[i + 1]))
            {
                (chars[i], chars[i + 1]) = (chars[i + 1], chars[i]);
                i++;
            }
        }
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
    /// Upper-cases (invariant culture) the first character of every space-separated
    /// word, leaving all other characters unchanged.
    /// </summary>
    public static string Capitalize(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        var chars = text.ToCharArray();
        for (var i = 0; i < chars.Length; i++)
        {
            if (chars[i] != ' ' && (i == 0 || chars[i - 1] == ' '))
            {
                chars[i] = char.ToUpperInvariant(chars[i]);
            }
        }
        return new string(chars);
    }
}

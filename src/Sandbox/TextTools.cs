namespace Sandbox;

public static class TextTools
{
    /// <summary>
    /// Counts words separated by runs of whitespace, ZERO WIDTH SPACE (U+200B) or
    /// ZERO WIDTH NO-BREAK SPACE / BYTE ORDER MARK (U+FEFF);
    /// text made only of separators has no words.
    /// </summary>
    public static int WordCount(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return 0;
        }
        // U+200B and U+FEFF are not char.IsWhiteSpace, so treat them as spaces explicitly.
        return text.Replace('﻿', ' ').Replace('​', ' ').Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    /// <summary>
    /// Reverses the text elements (grapheme clusters) of <paramref name="text"/>, so
    /// surrogate pairs, combining marks and "\r\n" line breaks stay in order.
    /// </summary>
    public static string Reverse(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        var builder = new System.Text.StringBuilder(text.Length);
        var starts = System.Globalization.StringInfo.ParseCombiningCharacters(text);
        for (var i = starts.Length - 1; i >= 0; i--)
        {
            var end = i + 1 < starts.Length ? starts[i + 1] : text.Length;
            builder.Append(text, starts[i], end - starts[i]);
        }
        return builder.ToString();
    }

    /// <summary>
    /// Counts lines ended by "\n", "\r\n", a lone "\r", LINE SEPARATOR (U+2028) or
    /// PARAGRAPH SEPARATOR (U+2029); a trailing terminator does not start an extra empty line.
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
            if (IsLineTerminator(text[i]))
            {
                lines++;
            }
        }
        if (text.Length > 0 && !IsLineTerminator(text[^1]))
        {
            lines++;
        }
        return lines;
    }

    private static bool IsLineTerminator(char c) =>
        c is '\r' or '\n' or (char)0x2028 or (char)0x2029;

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
    /// Upper-cases (invariant culture) the first character of every word separated by
    /// whitespace or ZERO WIDTH SPACE (U+200B), leaving all other characters unchanged.
    /// </summary>
    public static string Capitalize(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        var chars = text.ToCharArray();
        for (var i = 0; i < chars.Length; i++)
        {
            if (!char.IsWhiteSpace(chars[i]) && (i == 0 || char.IsWhiteSpace(chars[i - 1]) || chars[i - 1] == '​'))
            {
                // Upper-case the whole first code point so surrogate pairs are handled.
                if (System.Text.Rune.TryGetRuneAt(text, i, out var rune))
                {
                    var upper = System.Text.Rune.ToUpperInvariant(rune);
                    if (upper.Utf16SequenceLength == rune.Utf16SequenceLength)
                    {
                        upper.EncodeToUtf16(chars.AsSpan(i));
                    }
                }
            }
        }
        return new string(chars);
    }

    /// <summary>
    /// Whether <paramref name="text"/> is made only of whitespace and ZERO WIDTH SPACE (U+200B);
    /// empty text is blank.
    /// </summary>
    public static bool IsBlank(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        foreach (var ch in text)
        {
            if (!char.IsWhiteSpace(ch) && ch != '​')
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Returns the first word of <paramref name="text"/>, unchanged, where words are separated by
    /// whitespace or ZERO WIDTH SPACE (U+200B); blank text has no first word and yields "".
    /// </summary>
    public static string FirstWord(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        if (IsBlank(text))
        {
            return string.Empty;
        }
        var start = 0;
        while (char.IsWhiteSpace(text[start]) || text[start] == '​')
        {
            start++;
        }
        var end = start;
        while (end < text.Length && !char.IsWhiteSpace(text[end]) && text[end] != '​')
        {
            end++;
        }
        return text[start..end];
    }
}

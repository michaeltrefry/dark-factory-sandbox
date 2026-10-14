namespace Sandbox;

public static class TextTruncate
{
    /// <summary>
    /// Returns <paramref name="text"/> unchanged when it has at most <paramref name="maxElements"/>
    /// text elements (grapheme clusters); otherwise the first <paramref name="maxElements"/> - 1
    /// text elements followed by "…" (U+2026), so surrogate pairs, combining marks and "\r\n"
    /// line breaks are never split.
    /// </summary>
    public static string ToLength(string text, int maxElements)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentOutOfRangeException.ThrowIfLessThan(maxElements, 1);

        var starts = System.Globalization.StringInfo.ParseCombiningCharacters(text);
        if (starts.Length <= maxElements)
        {
            return text;
        }
        return string.Concat(text.AsSpan(0, starts[maxElements - 1]), "…");
    }
}

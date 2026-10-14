namespace Sandbox;

public static class TextCase
{
    /// <summary>
    /// Splits <paramref name="text"/> into words on whitespace, '-' and '_', and immediately before an
    /// ASCII uppercase letter that follows an ASCII lowercase letter or digit. Word order and casing are
    /// preserved; empty parts are dropped.
    /// </summary>
    public static string[] SplitWords(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        var words = new List<string>();
        var start = -1;
        for (var i = 0; i < text.Length; i++)
        {
            var ch = text[i];
            if (char.IsWhiteSpace(ch) || ch == '-' || ch == '_')
            {
                if (start >= 0)
                {
                    words.Add(text[start..i]);
                    start = -1;
                }
                continue;
            }
            if (start >= 0 && char.IsAsciiLetterUpper(ch)
                && (char.IsAsciiLetterLower(text[i - 1]) || char.IsAsciiDigit(text[i - 1])))
            {
                words.Add(text[start..i]);
                start = i;
            }
            else if (start < 0)
            {
                start = i;
            }
        }
        if (start >= 0)
        {
            words.Add(text[start..]);
        }
        return words.ToArray();
    }

    /// <summary>
    /// Converts <paramref name="text"/> to snake_case: the words from <see cref="SplitWords"/>,
    /// lowercased with the invariant culture and joined with '_'.
    /// </summary>
    public static string ToSnake(string text) =>
        string.Join('_', SplitWords(text).Select(word => word.ToLowerInvariant()));
}

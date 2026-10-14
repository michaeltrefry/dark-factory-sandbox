namespace Sandbox;

public static class TextCase
{
    /// <summary>
    /// Splits on whitespace, hyphens, underscores and before ASCII uppercase letters following
    /// ASCII lowercase letters or digits, preserving casing and all other characters.
    /// </summary>
    public static string[] SplitWords(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        var words = new List<string>();
        var start = 0;
        for (var i = 0; i < text.Length; i++)
        {
            var ch = text[i];
            if (char.IsWhiteSpace(ch) || ch is '-' or '_')
            {
                if (i > start)
                {
                    words.Add(text[start..i]);
                }
                start = i + 1;
            }
            else if (i > start && ch is >= 'A' and <= 'Z'
                && text[i - 1] is (>= 'a' and <= 'z') or (>= '0' and <= '9'))
            {
                words.Add(text[start..i]);
                start = i;
            }
        }
        if (start < text.Length)
        {
            words.Add(text[start..]);
        }
        return words.ToArray();
    }

    /// <summary>
    /// Joins the words from <see cref="SplitWords"/>, lower-cased invariantly, with underscores.
    /// </summary>
    public static string ToSnake(string text) =>
        string.Join('_', SplitWords(text).Select(word => word.ToLowerInvariant()));
}

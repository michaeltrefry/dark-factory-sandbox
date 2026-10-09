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
        // Reversing splits surrogate pairs into low-then-high; swap them back.
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
}

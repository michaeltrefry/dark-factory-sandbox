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

    /// <summary>Counts non-overlapping ordinal occurrences of <paramref name="value"/> in <paramref name="text"/>.</summary>
    public static int CountOccurrences(string text, string value)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(value);
        if (value.Length == 0)
        {
            throw new ArgumentException("Value must not be empty.", nameof(value));
        }

        var count = 0;
        var index = text.IndexOf(value, StringComparison.Ordinal);
        while (index >= 0)
        {
            count++;
            index = text.IndexOf(value, index + value.Length, StringComparison.Ordinal);
        }
        return count;
    }
}

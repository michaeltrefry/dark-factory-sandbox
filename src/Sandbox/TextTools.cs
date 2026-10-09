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
    /// Shortens <paramref name="text"/> to at most <paramref name="maxLength"/> characters,
    /// including a trailing <c>"..."</c>. Cuts at the last space before the limit when there
    /// is one (dropping the space), otherwise mid-word. Text that already fits is returned unchanged.
    /// </summary>
    /// <exception cref="ArgumentNullException"><paramref name="text"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxLength"/> is less than 3.</exception>
    public static string Truncate(string text, int maxLength)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentOutOfRangeException.ThrowIfLessThan(maxLength, 3);
        if (text.Length <= maxLength)
        {
            return text;
        }

        var budget = maxLength - 3;
        var cut = text.LastIndexOf(' ', budget);
        var head = cut > 0 ? text[..cut].TrimEnd(' ') : "";
        if (head.Length == 0)
        {
            head = text[..budget];
        }
        return head + "...";
    }
}

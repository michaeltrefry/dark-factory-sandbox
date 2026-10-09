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

    /// <summary>Counts the ASCII vowels a, e, i, o, u in <paramref name="text"/>, case-insensitive.</summary>
    public static int CountVowels(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        var count = 0;
        foreach (var c in text)
        {
            if ("aeiouAEIOU".Contains(c))
            {
                count++;
            }
        }
        return count;
    }
}

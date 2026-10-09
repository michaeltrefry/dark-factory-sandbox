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
    /// Returns true when the letters and digits of <paramref name="text"/>, compared
    /// case-insensitively, read the same backwards; all other characters are ignored.
    /// </summary>
    public static bool IsPalindrome(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        int i = 0, j = text.Length - 1;
        while (i < j)
        {
            if (!char.IsLetterOrDigit(text[i])) { i++; continue; }
            if (!char.IsLetterOrDigit(text[j])) { j--; continue; }
            if (char.ToUpperInvariant(text[i]) != char.ToUpperInvariant(text[j]))
            {
                return false;
            }
            i++;
            j--;
        }
        return true;
    }
}

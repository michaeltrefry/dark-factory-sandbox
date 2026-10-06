namespace Sandbox;

public static class TextTools
{
    /// <summary>
    /// Counts words separated by whitespace. Runs of whitespace count as a single
    /// separator, leading/trailing whitespace is ignored, and empty or
    /// whitespace-only input returns 0.
    /// </summary>
    public static int WordCount(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return 0;
        }
        return text.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    /// <summary>Reverses the characters of <paramref name="text"/>.</summary>
    public static string Reverse(string text)
    {
        var chars = text.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
}

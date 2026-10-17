namespace Sandbox;

public static class HotFileCount
{
    public static int Words(string text)
    {
        int count = 0;
        bool inWord = false;
        foreach (char c in text)
        {
            if (char.IsWhiteSpace(c))
            {
                inWord = false;
            }
            else if (!inWord)
            {
                inWord = true;
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// Returns the number of '\n'-separated lines in <paramref name="text"/>; a non-empty string has
    /// one line unless it contains a terminator, and a trailing terminator ends a line.
    /// </summary>
    public static int Lines(string text) => text.Count(c => c == '\n') + 1;
}

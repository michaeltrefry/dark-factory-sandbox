namespace Sandbox;

public static class HotFileCount
{
    public static int Words(string text) => text.Split(' ').Length;

    /// <summary>
    /// Returns the number of '\n'-separated lines in <paramref name="text"/>; a non-empty string has
    /// one line unless it contains a terminator, and a trailing terminator ends a line.
    /// </summary>
    public static int Lines(string text) => text.Count(c => c == '\n') + 1;
}

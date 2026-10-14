namespace Sandbox;

public static class Slug
{
    /// <summary>
    /// Lower-cases (invariant culture) <paramref name="text"/> and replaces each run of whitespace,
    /// ZERO WIDTH SPACE (U+200B) or ZERO WIDTH NO-BREAK SPACE / BYTE ORDER MARK (U+FEFF) with one
    /// hyphen, omitting leading and trailing separator runs; other characters are kept.
    /// </summary>
    public static string Normalize(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        var lower = text.ToLowerInvariant();
        var builder = new System.Text.StringBuilder(lower.Length);
        var pendingSeparator = false;
        foreach (var ch in lower)
        {
            if (char.IsWhiteSpace(ch) || ch == '​' || ch == '﻿')
            {
                pendingSeparator = builder.Length > 0;
                continue;
            }
            if (pendingSeparator)
            {
                builder.Append('-');
                pendingSeparator = false;
            }
            builder.Append(ch);
        }
        return builder.ToString();
    }
}

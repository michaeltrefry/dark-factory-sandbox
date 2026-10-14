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

    /// <summary>
    /// <see cref="Normalize"/>s <paramref name="text"/>, keeps only ASCII letters, ASCII digits and
    /// hyphens, then collapses each run of hyphens to one and trims hyphens from both ends.
    /// </summary>
    public static string From(string text)
    {
        var normalized = Normalize(text);
        var builder = new System.Text.StringBuilder(normalized.Length);
        var pendingHyphen = false;
        foreach (var ch in normalized)
        {
            if (ch == '-')
            {
                pendingHyphen = builder.Length > 0;
                continue;
            }
            if (!((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9')))
            {
                continue;
            }
            if (pendingHyphen)
            {
                builder.Append('-');
                pendingHyphen = false;
            }
            builder.Append(ch);
        }
        return builder.ToString();
    }

    /// <summary>
    /// Returns <see cref="From"/>(<paramref name="text"/>) when it is not in <paramref name="taken"/>;
    /// otherwise appends the first of -2, -3, ... that yields an unused slug. Does not modify
    /// <paramref name="taken"/>.
    /// </summary>
    public static string Unique(string text, IReadOnlyCollection<string> taken)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(taken);

        var slug = From(text);
        if (slug.Length == 0)
        {
            throw new ArgumentException("Text produces an empty slug.", nameof(text));
        }
        if (!taken.Contains(slug))
        {
            return slug;
        }
        for (var suffix = 2; ; suffix++)
        {
            var candidate = $"{slug}-{suffix}";
            if (!taken.Contains(candidate))
            {
                return candidate;
            }
        }
    }
}

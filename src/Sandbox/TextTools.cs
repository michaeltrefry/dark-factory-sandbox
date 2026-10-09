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

    /// <summary>Converts a PascalCase/camelCase identifier to lower snake_case.</summary>
    public static string ToSnakeCase(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        var sb = new System.Text.StringBuilder(text.Length + 4);
        for (var i = 0; i < text.Length; i++)
        {
            var c = text[i];
            if (char.IsUpper(c) && i > 0 && text[i - 1] != '_')
            {
                var prev = text[i - 1];
                var nextIsLower = i + 1 < text.Length && char.IsLower(text[i + 1]);
                if (char.IsLower(prev) || char.IsDigit(prev) || (char.IsUpper(prev) && nextIsLower))
                {
                    sb.Append('_');
                }
            }
            sb.Append(char.ToLowerInvariant(c));
        }
        return sb.ToString();
    }
}

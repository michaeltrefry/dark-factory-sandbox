using System.Text;

namespace Sandbox;

public static class TextInitials
{
    /// <summary>
    /// Joins the first code point of each word from <see cref="TextTools.Words"/>, upper-cased
    /// with <see cref="Rune.ToUpperInvariant"/>; text with no words yields "".
    /// </summary>
    public static string Of(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        var builder = new StringBuilder();
        foreach (var word in TextTools.Words(text))
        {
            if (Rune.TryGetRuneAt(word, 0, out var rune))
            {
                builder.Append(Rune.ToUpperInvariant(rune).ToString());
            }
            else
            {
                // A lone surrogate is not a valid code point; keep it as is.
                builder.Append(word[0]);
            }
        }
        return builder.ToString();
    }
}

using System.Globalization;

namespace Sandbox;

/// <summary>
/// Column alignment for <see cref="MarkdownTable.Format"/>.
/// </summary>
public enum Alignment
{
    None,
    Left,
    Center,
    Right,
}

public static class MarkdownTable
{
    /// <summary>
    /// Renders <paramref name="header"/> and <paramref name="rows"/> as a GitHub-flavoured Markdown
    /// table. Each column is padded to its widest cell (header included), measured in text elements
    /// so emoji and combining marks count as one. A <c>|</c> in a cell is escaped as <c>\|</c> and
    /// line breaks become <c>&lt;br&gt;</c>. Rows shorter than the header are padded with empty
    /// cells. Lines are joined with <c>\n</c>, with no trailing newline.
    /// </summary>
    public static string Format(
        IReadOnlyList<string> header,
        IReadOnlyList<IReadOnlyList<string>> rows,
        IReadOnlyList<Alignment>? alignments = null)
    {
        ArgumentNullException.ThrowIfNull(header);
        ArgumentNullException.ThrowIfNull(rows);
        if (header.Count == 0)
        {
            throw new ArgumentException("Header must contain at least one column.", nameof(header));
        }
        if (alignments is not null && alignments.Count != header.Count)
        {
            throw new ArgumentException("Alignments must have one entry per column.", nameof(alignments));
        }

        var columns = header.Count;
        var alignment = new Alignment[columns];
        if (alignments is not null)
        {
            for (var c = 0; c < columns; c++)
            {
                alignment[c] = alignments[c];
            }
        }

        var cells = new string[rows.Count][];
        var widths = new int[columns];
        var escapedHeader = new string[columns];
        for (var c = 0; c < columns; c++)
        {
            escapedHeader[c] = Escape(header[c]);
            widths[c] = TextWidth(escapedHeader[c]);
        }

        for (var r = 0; r < rows.Count; r++)
        {
            var row = rows[r];
            ArgumentNullException.ThrowIfNull(row);
            if (row.Count > columns)
            {
                throw new ArgumentException($"Row {r} has {row.Count} cells but the header has {columns} columns.", nameof(rows));
            }
            var escaped = new string[columns];
            for (var c = 0; c < columns; c++)
            {
                escaped[c] = c < row.Count ? Escape(row[c]) : "";
                widths[c] = Math.Max(widths[c], TextWidth(escaped[c]));
            }
            cells[r] = escaped;
        }

        var lines = new List<string>(rows.Count + 2)
        {
            RenderRow(escapedHeader, widths, alignment),
            RenderSeparator(widths, alignment),
        };
        foreach (var row in cells)
        {
            lines.Add(RenderRow(row, widths, alignment));
        }
        return string.Join("\n", lines);
    }

    private static string Escape(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return "";
        }
        return text
            .Replace("|", "\\|")
            .Replace("\r\n", "<br>")
            .Replace("\n", "<br>")
            .Replace("\r", "<br>");
    }

    private static int TextWidth(string text) => new StringInfo(text).LengthInTextElements;

    private static string Pad(string text, int width, Alignment alignment)
    {
        var extra = width - TextWidth(text);
        if (extra <= 0)
        {
            return text;
        }
        return alignment switch
        {
            Alignment.Right => new string(' ', extra) + text,
            Alignment.Center => new string(' ', extra / 2) + text + new string(' ', extra - extra / 2),
            _ => text + new string(' ', extra),
        };
    }

    private static string RenderRow(IReadOnlyList<string> cells, int[] widths, Alignment[] alignment)
    {
        var parts = new string[cells.Count];
        for (var c = 0; c < cells.Count; c++)
        {
            parts[c] = Pad(cells[c], widths[c], alignment[c]);
        }
        return "| " + string.Join(" | ", parts) + " |";
    }

    private static string RenderSeparator(int[] widths, Alignment[] alignment)
    {
        var parts = new string[widths.Length];
        for (var c = 0; c < widths.Length; c++)
        {
            parts[c] = alignment[c] switch
            {
                Alignment.Left => ":" + new string('-', Math.Max(widths[c] - 1, 3)),
                Alignment.Center => ":" + new string('-', Math.Max(widths[c] - 2, 3)) + ":",
                Alignment.Right => new string('-', Math.Max(widths[c] - 1, 3)) + ":",
                _ => new string('-', Math.Max(widths[c], 3)),
            };
        }
        return "| " + string.Join(" | ", parts) + " |";
    }
}

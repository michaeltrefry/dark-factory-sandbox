namespace Sandbox.Tests;

public class MarkdownTableTests
{
    // Rule 1: every column is padded to its widest cell (header included).

    [Fact]
    public void Format_pads_columns_to_widest_cell_including_header()
    {
        var result = MarkdownTable.Format(
            ["Name", "Note"],
            [["A", "🎉"], ["Long name", "x"]]);
        Assert.Equal(
            "| Name      | Note |\n" +
            "| --------- | ---- |\n" +
            "| A         | 🎉    |\n" +
            "| Long name | x    |",
            result);
    }

    [Fact]
    public void Format_measures_width_in_text_elements_so_combining_marks_count_as_one()
    {
        const string combined = "é"; // "e" + COMBINING ACUTE ACCENT: two chars, one text element
        Assert.Equal(2, combined.Length);
        var result = MarkdownTable.Format(["X"], [[combined], ["ab"]]);
        Assert.Equal(
            "| X  |\n" +
            "| --- |\n" +
            $"| {combined}  |\n" +
            "| ab |",
            result);
    }

    [Fact]
    public void Format_counts_emoji_zwj_sequence_as_one_text_element()
    {
        var result = MarkdownTable.Format(["H"], [["👨‍👩‍👧"], ["ab"]]);
        Assert.Equal(
            "| H  |\n" +
            "| --- |\n" +
            "| 👨‍👩‍👧  |\n" +
            "| ab |",
            result);
    }

    // Rule 2: the separator row reflects alignment and is as wide as the column, minimum 3 dashes.

    [Fact]
    public void Format_separator_reflects_alignment_with_minimum_three_dashes()
    {
        var result = MarkdownTable.Format(
            ["A", "B", "C", "D"],
            [["1", "2", "3", "4"]],
            [Alignment.Left, Alignment.Center, Alignment.Right, Alignment.None]);
        Assert.Equal("| :--- | :---: | ---: | --- |", result.Split('\n')[1]);
    }

    [Theory]
    [InlineData(Alignment.Left, ":----------")]
    [InlineData(Alignment.Center, ":---------:")]
    [InlineData(Alignment.Right, "----------:")]
    [InlineData(Alignment.None, "-----------")]
    public void Format_separator_is_as_wide_as_the_column(Alignment alignment, string expected)
    {
        var result = MarkdownTable.Format(["Wide column"], [], [alignment]);
        Assert.Equal($"| {expected} |", result.Split('\n')[1]);
    }

    // Rule 3: cell text is padded according to alignment.

    [Fact]
    public void Format_pads_cells_according_to_alignment()
    {
        var result = MarkdownTable.Format(
            ["L", "C", "R"],
            [["xxx", "xxx", "xxx"]],
            [Alignment.Left, Alignment.Center, Alignment.Right]);
        Assert.Equal(
            "| L   |  C  |   R |\n" +
            "| :--- | :---: | ---: |\n" +
            "| xxx | xxx | xxx |",
            result);
    }

    [Fact]
    public void Format_center_alignment_puts_the_extra_space_on_the_right()
    {
        var result = MarkdownTable.Format(["CC"], [["abcde"]], [Alignment.Center]);
        Assert.Equal(
            "|  CC   |\n" +
            "| :---: |\n" +
            "| abcde |",
            result);
    }

    // Rule 4: escaping.

    [Fact]
    public void Format_escapes_pipes_and_converts_line_breaks_to_br()
    {
        var result = MarkdownTable.Format(
            ["A|B", "C"],
            [["x\ny", "a|b"], ["p\r\nq", ""]]);
        Assert.Equal(
            "| A\\|B   | C    |\n" +
            "| ------ | ---- |\n" +
            "| x<br>y | a\\|b |\n" +
            "| p<br>q |      |",
            result);
    }

    // Rule 5: row length handling.

    [Fact]
    public void Format_pads_short_rows_with_empty_cells()
    {
        var result = MarkdownTable.Format(["A", "B", "C"], [["1"]]);
        Assert.Equal(
            "| A | B | C |\n" +
            "| --- | --- | --- |\n" +
            "| 1 |   |   |",
            result);
    }

    [Fact]
    public void Format_throws_when_a_row_is_longer_than_the_header()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            MarkdownTable.Format(["A", "B"], [["1", "2", "3"]]));
        Assert.Equal("rows", ex.ParamName);
    }

    // Rule 6: argument validation.

    [Fact]
    public void Format_throws_on_null_header()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => MarkdownTable.Format(null!, []));
        Assert.Equal("header", ex.ParamName);
    }

    [Fact]
    public void Format_throws_on_null_rows()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => MarkdownTable.Format(["A"], null!));
        Assert.Equal("rows", ex.ParamName);
    }

    [Fact]
    public void Format_throws_on_empty_header()
    {
        var ex = Assert.Throws<ArgumentException>(() => MarkdownTable.Format([], []));
        Assert.Equal("header", ex.ParamName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    public void Format_throws_when_alignments_length_differs_from_header(int count)
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            MarkdownTable.Format(["A", "B"], [], new Alignment[count]));
        Assert.Equal("alignments", ex.ParamName);
    }

    // Rule 7: line joining.

    [Fact]
    public void Format_joins_lines_with_lf_and_has_no_trailing_newline()
    {
        var result = MarkdownTable.Format(["A"], [["1"], ["2"]]);
        Assert.Equal(4, result.Split('\n').Length);
        Assert.DoesNotContain('\r', result);
        Assert.False(result.EndsWith('\n'));
    }

    // Full example.

    [Fact]
    public void Format_renders_a_full_example_table()
    {
        var result = MarkdownTable.Format(
            ["Name", "Role", "Score"],
            [
                ["Ada Lovelace", "admin", "9.5"],
                ["Grace", "user|ops", "10"],
                ["Émile\nZola", "user", "7.25"],
            ],
            [Alignment.Left, Alignment.Center, Alignment.Right]);
        Assert.Equal(
            "| Name          |   Role    | Score |\n" +
            "| :------------ | :-------: | ----: |\n" +
            "| Ada Lovelace  |   admin   |   9.5 |\n" +
            "| Grace         | user\\|ops |    10 |\n" +
            "| Émile<br>Zola |   user    |  7.25 |",
            result);
    }
}

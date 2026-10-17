namespace Sandbox.Tests;

public class HotFileCountTests
{
    [Fact]
    public void Words_counts_a_b() =>
        Assert.Equal(2, HotFileCount.Words("a b"));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData(" \t\n ")]
    public void Words_returns_0_for_empty_or_whitespace_only(string text) =>
        Assert.Equal(0, HotFileCount.Words(text));

    [Fact]
    public void Words_ignores_boundary_whitespace_and_repeated_separators() =>
        Assert.Equal(2, HotFileCount.Words("  a   b  "));

    [Fact]
    public void Words_separates_on_mixed_whitespace() =>
        Assert.Equal(4, HotFileCount.Words("a\tb\nc\r\nd"));

    [Theory]
    [InlineData("", 1)]
    [InlineData("a", 1)]
    [InlineData("a\nb", 2)]
    [InlineData("a\nb\nc", 3)]
    [InlineData("a\n", 2)]
    public void Lines_counts_newline_separated_lines(string text, int expected) =>
        Assert.Equal(expected, HotFileCount.Lines(text));
}

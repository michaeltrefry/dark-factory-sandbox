namespace Sandbox.Tests;

public class HotFileCountTests
{
    [Fact]
    public void Words_counts_a_b() =>
        Assert.Equal(2, HotFileCount.Words("a b"));

    [Theory]
    [InlineData("", 1)]
    [InlineData("a", 1)]
    [InlineData("a\nb", 2)]
    [InlineData("a\nb\nc", 3)]
    [InlineData("a\n", 2)]
    public void Lines_counts_newline_separated_lines(string text, int expected) =>
        Assert.Equal(expected, HotFileCount.Lines(text));
}

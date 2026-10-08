namespace Sandbox.Tests;

public class TextToolsTests
{
    [Theory]
    [InlineData("", 0)]
    [InlineData("hello", 1)]
    [InlineData("hello world", 2)]
    public void WordCount_counts_single_spaced_words(string text, int expected) =>
        Assert.Equal(expected, TextTools.WordCount(text));

    [Theory]
    [InlineData(" ", 0)]
    [InlineData("\t", 0)]
    [InlineData("\n", 0)]
    [InlineData("\r\n", 0)]
    [InlineData(" ", 0)]
    [InlineData(" \t\n", 0)]
    [InlineData("\r\n\t  ", 0)]
    [InlineData("a  b", 2)]
    [InlineData("a\tb", 2)]
    [InlineData("a\nb", 2)]
    [InlineData("a\r\nb", 2)]
    [InlineData("a b", 2)]
    [InlineData("a\rb", 2)]
    [InlineData("a\vb", 2)]
    [InlineData("a\fb", 2)]
    [InlineData("a b", 2)]
    [InlineData(" \t\r\n hello \n\t ", 1)]
    [InlineData("\t a\r\nb   c\n", 3)]
    [InlineData("First line has words.\nSecond\tline has more.\r\n\r\nLast line.", 10)]
    public void WordCount_counts_runs_of_non_whitespace(string text, int expected) =>
        Assert.Equal(expected, TextTools.WordCount(text));

    [Fact]
    public void Reverse_reverses() => Assert.Equal("cba", TextTools.Reverse("abc"));
}

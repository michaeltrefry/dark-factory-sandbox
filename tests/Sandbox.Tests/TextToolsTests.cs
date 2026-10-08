namespace Sandbox.Tests;

public class TextToolsTests
{
    [Theory]
    [InlineData("", 0)]
    [InlineData("hello", 1)]
    [InlineData("hello world", 2)]
    public void WordCount_counts_single_spaced_words(string text, int expected) =>
        Assert.Equal(expected, TextTools.WordCount(text));

    [Fact]
    public void Reverse_reverses() => Assert.Equal("cba", TextTools.Reverse("abc"));

    [Theory]
    [InlineData("")]
    [InlineData("a")]
    public void Reverse_returns_short_strings_unchanged(string text)
    {
        var result = TextTools.Reverse(text);
        Assert.Equal(text, result);
        Assert.Same(text, result);
    }
}

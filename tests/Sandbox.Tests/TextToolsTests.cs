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
    [InlineData("a\U0001F600b", "b\U0001F600a")]
    [InlineData("\U0001F600\U0001F601", "\U0001F601\U0001F600")]
    [InlineData("\U0001F600", "\U0001F600")]
    public void Reverse_keeps_surrogate_pairs_together(string text, string expected) =>
        Assert.Equal(expected, TextTools.Reverse(text));
}

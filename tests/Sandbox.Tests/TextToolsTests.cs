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
    [InlineData("ab", 3, "ababab")]
    [InlineData("ab", 1, "ab")]
    [InlineData("ab", 0, "")]
    [InlineData("", 5, "")]
    public void Repeat_concatenates_text(string text, int count, string expected) =>
        Assert.Equal(expected, TextTools.Repeat(text, count));

    [Fact]
    public void Repeat_negative_count_throws()
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => TextTools.Repeat("ab", -1));
        Assert.Equal("count", ex.ParamName);
    }

    [Fact]
    public void Repeat_null_text_throws()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.Repeat(null!, 2));
        Assert.Equal("text", ex.ParamName);
    }
}

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

    [Fact]
    public void Truncate_returns_short_text_unchanged() =>
        Assert.Equal("hello", TextTools.Truncate("hello", 10));

    [Fact]
    public void Truncate_returns_text_of_exact_length_unchanged() =>
        Assert.Equal("hello world", TextTools.Truncate("hello world", 11));

    [Fact]
    public void Truncate_cuts_at_last_space_before_limit() =>
        Assert.Equal("hello...", TextTools.Truncate("hello world foo", 10));

    [Fact]
    public void Truncate_cuts_mid_word_when_no_space() =>
        Assert.Equal("abcdefg...", TextTools.Truncate("abcdefghijklmnop", 10));

    [Fact]
    public void Truncate_throws_when_maxLength_below_three()
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => TextTools.Truncate("hello", 2));
        Assert.Equal("maxLength", ex.ParamName);
    }

    [Fact]
    public void Truncate_throws_when_text_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.Truncate(null!, 10));
        Assert.Equal("text", ex.ParamName);
    }
}

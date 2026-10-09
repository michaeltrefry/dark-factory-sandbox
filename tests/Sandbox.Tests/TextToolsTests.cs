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
    [InlineData("aaaa", "aa", 2)]
    [InlineData("abc", "d", 0)]
    [InlineData("abcabc", "abc", 2)]
    [InlineData("", "a", 0)]
    [InlineData("aA", "a", 1)]
    public void CountOccurrences_counts_non_overlapping_ordinal_matches(string text, string value, int expected) =>
        Assert.Equal(expected, TextTools.CountOccurrences(text, value));

    [Fact]
    public void CountOccurrences_throws_on_empty_value()
    {
        var ex = Assert.Throws<ArgumentException>(() => TextTools.CountOccurrences("abc", ""));
        Assert.Equal("value", ex.ParamName);
    }

    [Fact]
    public void CountOccurrences_throws_on_null_arguments()
    {
        Assert.Equal("text", Assert.Throws<ArgumentNullException>(() => TextTools.CountOccurrences(null!, "a")).ParamName);
        Assert.Equal("value", Assert.Throws<ArgumentNullException>(() => TextTools.CountOccurrences("a", null!)).ParamName);
    }
}

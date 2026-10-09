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
    [InlineData("ada lovelace", "AL")]
    [InlineData("  ada   lovelace  ", "AL")]
    [InlineData("grace", "G")]
    [InlineData("", "")]
    [InlineData("   ", "")]
    public void Initials_returns_upper_cased_first_letters(string name, string expected) =>
        Assert.Equal(expected, TextTools.Initials(name));

    [Fact]
    public void Initials_null_throws() =>
        Assert.Equal("name", Assert.Throws<ArgumentNullException>(() => TextTools.Initials(null!)).ParamName);
}

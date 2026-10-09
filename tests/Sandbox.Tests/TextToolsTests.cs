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
    [InlineData("", 0)]
    [InlineData("Education", 5)]
    [InlineData("rhythm", 0)]
    [InlineData("AEIOU aeiou", 10)]
    public void CountVowels_counts_ascii_vowels_case_insensitively(string text, int expected) =>
        Assert.Equal(expected, TextTools.CountVowels(text));

    [Fact]
    public void CountVowels_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.CountVowels(null!));
        Assert.Equal("text", ex.ParamName);
    }
}

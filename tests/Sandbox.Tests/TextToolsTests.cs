namespace Sandbox.Tests;

public class TextToolsTests
{
    [Theory]
    [InlineData("", 0)]
    [InlineData("hello", 1)]
    [InlineData("hello world", 2)]
    [InlineData("   ", 0)]
    [InlineData("hello   world", 2)]
    [InlineData("  hello world  ", 2)]
    [InlineData("hello\tworld", 2)]
    [InlineData("hello\nworld\r\nagain", 3)]
    [InlineData("\t one  two \n three \t", 3)]
    public void WordCount_counts_whitespace_separated_words(string text, int expected) =>
        Assert.Equal(expected, TextTools.WordCount(text));

    [Fact]
    public void Reverse_reverses() => Assert.Equal("cba", TextTools.Reverse("abc"));
}

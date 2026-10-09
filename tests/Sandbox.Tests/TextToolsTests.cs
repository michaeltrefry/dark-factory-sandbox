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
    [InlineData("", "")]
    [InlineData("hello", "Hello")]
    [InlineData("hello wORLD", "Hello WORLD")]
    [InlineData("  a  b ", "  A  B ")]
    [InlineData("1st place", "1st Place")]
    public void Capitalize_upper_cases_first_letter_of_each_word(string text, string expected) =>
        Assert.Equal(expected, TextTools.Capitalize(text));

    [Fact]
    public void Capitalize_null_throws()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.Capitalize(null!));
        Assert.Equal("text", ex.ParamName);
    }
}

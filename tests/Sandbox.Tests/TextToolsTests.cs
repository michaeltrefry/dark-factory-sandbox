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
    [InlineData("a", 1)]
    [InlineData("a\nb", 2)]
    [InlineData("a\r\nb\r\n", 2)]
    [InlineData("a\rb", 2)]
    [InlineData("a\n", 1)]
    [InlineData("\n", 1)]
    [InlineData("\n\n", 2)]
    [InlineData("a\r\n\rb", 3)]
    public void CountLines_counts_lines(string text, int expected) =>
        Assert.Equal(expected, TextTools.CountLines(text));

    [Fact]
    public void CountLines_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.CountLines(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Theory]
    [InlineData("banana", 'a', 3)]
    [InlineData("", 'x', 0)]
    [InlineData("abc", 'x', 0)]
    [InlineData("Aa", 'a', 1)]
    public void CountChar_counts_ordinal_occurrences(string text, char c, int expected) =>
        Assert.Equal(expected, TextTools.CountChar(text, c));

    [Fact]
    public void CountChar_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.CountChar(null!, 'a'));
        Assert.Equal("text", ex.ParamName);
    }

    [Theory]
    [InlineData("Hello", "he", true)]
    [InlineData("Hello", "HELLO", true)]
    [InlineData("Hi", "hello", false)]
    [InlineData("Hello", "", true)]
    [InlineData("", "", true)]
    [InlineData("", "a", false)]
    [InlineData("abc", "bc", false)]
    public void StartsWithIgnoreCase_compares_ordinal_ignoring_case(string text, string prefix, bool expected) =>
        Assert.Equal(expected, TextTools.StartsWithIgnoreCase(text, prefix));

    [Fact]
    public void StartsWithIgnoreCase_throws_on_null_text()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.StartsWithIgnoreCase(null!, "a"));
        Assert.Equal("text", ex.ParamName);
    }

    [Fact]
    public void StartsWithIgnoreCase_throws_on_null_prefix()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.StartsWithIgnoreCase("a", null!));
        Assert.Equal("prefix", ex.ParamName);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("abc", "abc")]
    [InlineData("a b", "a b")]
    [InlineData("a   b  c", "a b c")]
    [InlineData("  a", " a")]
    [InlineData("a   ", "a ")]
    [InlineData("   ", " ")]
    [InlineData("a\t\tb", "a\t\tb")]
    public void Squeeze_collapses_runs_of_spaces(string text, string expected) =>
        Assert.Equal(expected, TextTools.Squeeze(text));

    [Fact]
    public void Squeeze_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.Squeeze(null!));
        Assert.Equal("text", ex.ParamName);
    }
}

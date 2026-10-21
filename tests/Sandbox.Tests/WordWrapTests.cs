namespace Sandbox.Tests;

public class WordWrapTests
{
    [Fact]
    public void Wrap_throws_on_null_text()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => WordWrap.Wrap(null!, 5));
        Assert.Equal("text", ex.ParamName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Wrap_throws_on_width_below_one(int width)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => WordWrap.Wrap("text", width));
        Assert.Equal("width", ex.ParamName);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("   ", "")]
    [InlineData("hello", "hello")]
    [InlineData("hello world", "hello\nworld")]
    public void Wrap_breaks_lines_only_when_needed(string text, string expected) =>
        Assert.Equal(expected, WordWrap.Wrap(text, 5));

    [Fact]
    public void Wrap_fits_as_many_words_as_the_width_allows() =>
        Assert.Equal("aaa bbb\nccc", WordWrap.Wrap("aaa bbb ccc", 7));

    [Fact]
    public void Wrap_keeps_a_line_at_exactly_the_width() =>
        Assert.Equal("aaa bbb", WordWrap.Wrap("aaa bbb", 7));

    [Fact]
    public void Wrap_keeps_words_longer_than_the_width_on_their_own_line() =>
        Assert.Equal("abcdefghij\nab", WordWrap.Wrap("abcdefghij ab", 5));

    [Fact]
    public void Wrap_keeps_successive_oversized_words_on_separate_lines() =>
        Assert.Equal("abcdef\nghijkl\nab", WordWrap.Wrap("abcdef ghijkl ab", 5));

    [Fact]
    public void Wrap_puts_a_fitting_word_after_an_oversized_word_on_a_new_line() =>
        Assert.Equal("abcdef\nab cd", WordWrap.Wrap("abcdef ab cd", 5));

    [Fact]
    public void Wrap_collapses_runs_of_whitespace_into_single_spaces() =>
        Assert.Equal("a b\nc", WordWrap.Wrap("a \t\r\n b  c", 3));

    [Fact]
    public void Wrap_ignores_leading_and_trailing_whitespace() =>
        Assert.Equal("a b", WordWrap.Wrap("  a b\t", 5));

    [Fact]
    public void Wrap_wraps_a_long_sentence() =>
        Assert.Equal("The quick\nbrown fox\njumps", WordWrap.Wrap("The quick brown fox jumps", 10));
}

namespace Sandbox.Tests;

public class TextTruncateTests
{
    [Fact]
    public void ToLength_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTruncate.ToLength(null!, 3));
        Assert.Equal("text", ex.ParamName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ToLength_throws_when_max_elements_is_less_than_one(int maxElements)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => TextTruncate.ToLength("abc", maxElements));
        Assert.Equal("maxElements", ex.ParamName);
    }

    [Fact]
    public void ToLength_truncates_plain_ascii_with_ellipsis() =>
        Assert.Equal("hello…", TextTruncate.ToLength("hello world", 6));

    [Theory]
    [InlineData("hello", 5)]
    [InlineData("hello", 9)]
    [InlineData("", 1)]
    public void ToLength_returns_text_unchanged_when_it_fits(string text, int maxElements) =>
        Assert.Same(text, TextTruncate.ToLength(text, maxElements));

    [Fact]
    public void ToLength_keeps_surrogate_pair_at_cut_whole() =>
        Assert.Equal("a😀…", TextTruncate.ToLength("a😀😀b", 3));

    [Fact]
    public void ToLength_keeps_combining_mark_with_its_base_at_cut() =>
        Assert.Equal("aé…", TextTruncate.ToLength("aééx", 3));

    [Fact]
    public void ToLength_keeps_crlf_at_cut_whole() =>
        Assert.Equal("a\r\n…", TextTruncate.ToLength("a\r\nbc", 3));

    [Theory]
    [InlineData("ab")]
    [InlineData("😀😀")]
    [InlineData("éé")]
    public void ToLength_one_element_yields_only_ellipsis_for_longer_text(string text) =>
        Assert.Equal("…", TextTruncate.ToLength(text, 1));
}

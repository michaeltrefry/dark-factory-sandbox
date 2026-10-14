namespace Sandbox.Tests;

public class TextCaseTests
{
    [Fact]
    public void SplitWords_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextCase.SplitWords(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Fact]
    public void SplitWords_splits_on_case_boundaries_and_underscore() =>
        Assert.Equal(["parse", "Http", "Request", "v2"], TextCase.SplitWords("parseHttpRequest_v2"));

    [Fact]
    public void SplitWords_splits_before_uppercase_after_digit() =>
        Assert.Equal(["v2", "Value"], TextCase.SplitWords("v2Value"));

    [Theory]
    [InlineData("a b")]
    [InlineData("a\tb")]
    [InlineData("a\r\nb")]
    [InlineData("a b")]
    [InlineData("a b")]
    [InlineData("a-b")]
    [InlineData("a_b")]
    [InlineData("  a -_\t b__ ")]
    [InlineData("--a--b--")]
    public void SplitWords_splits_on_separators_without_empty_parts(string text) =>
        Assert.Equal(["a", "b"], TextCase.SplitWords(text));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t\r\n")]
    [InlineData("-_- _")]
    public void SplitWords_separator_only_text_is_empty(string text) =>
        Assert.Empty(TextCase.SplitWords(text));

    [Theory]
    [InlineData("HTTPServer")]
    [InlineData("ABC")]
    [InlineData("Hello")]
    [InlineData("éA")]
    [InlineData("aÉ")]
    [InlineData("a​b")]
    public void SplitWords_keeps_single_word(string text) =>
        Assert.Equal([text], TextCase.SplitWords(text));

    [Fact]
    public void SplitWords_preserves_order_and_casing() =>
        Assert.Equal(["Foo", "BAR", "baz", "Qux"], TextCase.SplitWords("Foo BAR-baz_Qux"));
}

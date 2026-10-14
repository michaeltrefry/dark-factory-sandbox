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

    [Fact]
    public void ToSnake_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextCase.ToSnake(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Theory]
    [InlineData("parseHttpRequest", "parse_http_request")]
    [InlineData("parseHttpRequest_v2", "parse_http_request_v2")]
    [InlineData(" max--retryCount__ ", "max_retry_count")]
    public void ToSnake_joins_lowercased_words_with_underscore(string text, string expected) =>
        Assert.Equal(expected, TextCase.ToSnake(text));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("-_- _")]
    public void ToSnake_separator_only_text_is_empty(string text) =>
        Assert.Equal("", TextCase.ToSnake(text));

    [Fact]
    public void ToSnake_lowercases_with_invariant_culture()
    {
        var original = System.Globalization.CultureInfo.CurrentCulture;
        try
        {
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("tr-TR");
            Assert.Equal("itemvalue", TextCase.ToSnake("ITEMValue"));
        }
        finally
        {
            System.Globalization.CultureInfo.CurrentCulture = original;
        }
    }

    [Fact]
    public void ToScreamingSnake_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextCase.ToScreamingSnake(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Theory]
    [InlineData("max-retryCount", "MAX_RETRY_COUNT")]
    [InlineData("parseHttpRequest_v2", "PARSE_HTTP_REQUEST_V2")]
    public void ToScreamingSnake_joins_uppercased_words_with_underscore(string text, string expected) =>
        Assert.Equal(expected, TextCase.ToScreamingSnake(text));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t\r\n")]
    [InlineData("-_- _")]
    public void ToScreamingSnake_separator_only_text_is_empty(string text) =>
        Assert.Equal("", TextCase.ToScreamingSnake(text));

    [Fact]
    public void ToScreamingSnake_uppercases_with_invariant_culture()
    {
        var original = System.Globalization.CultureInfo.CurrentCulture;
        try
        {
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("tr-TR");
            Assert.Equal("MINI_VALUE", TextCase.ToScreamingSnake("miniValue"));
        }
        finally
        {
            System.Globalization.CultureInfo.CurrentCulture = original;
        }
    }
}

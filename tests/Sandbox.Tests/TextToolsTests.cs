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
    [InlineData("HelloWorld", "hello_world")]
    [InlineData("helloWorld", "hello_world")]
    [InlineData("parseHTTPRequest", "parse_http_request")]
    [InlineData("already_snake", "already_snake")]
    [InlineData("XMLParser", "xml_parser")]
    [InlineData("getID", "get_id")]
    [InlineData("Version2Update", "version2_update")]
    public void ToSnakeCase_converts_identifiers(string text, string expected) =>
        Assert.Equal(expected, TextTools.ToSnakeCase(text));

    [Fact]
    public void ToSnakeCase_null_throws()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.ToSnakeCase(null!));
        Assert.Equal("text", ex.ParamName);
    }
}

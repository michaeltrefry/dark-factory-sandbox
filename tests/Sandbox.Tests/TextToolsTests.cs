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
    [InlineData("A man, a plan, a canal: Panama", true)]
    [InlineData("", true)]
    [InlineData("abc", false)]
    [InlineData("No 'x' in Nixon", true)]
    [InlineData("12321", true)]
    [InlineData("123ab", false)]
    public void IsPalindrome_ignores_case_and_non_alphanumerics(string text, bool expected) =>
        Assert.Equal(expected, TextTools.IsPalindrome(text));

    [Fact]
    public void IsPalindrome_null_throws()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.IsPalindrome(null!));
        Assert.Equal("text", ex.ParamName);
    }
}

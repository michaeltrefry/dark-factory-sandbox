namespace Sandbox.Tests;

public class TextSubmittedTests
{
    [Fact]
    public void TextSubmitted_throws_on_empty_or_whitespace_text()
    {
        foreach (var text in new[] { "   ", "" })
        {
            var ex = Assert.Throws<ArgumentException>(() => new TextSubmitted(text));
            Assert.Equal("text", ex.ParamName);
        }
    }
}

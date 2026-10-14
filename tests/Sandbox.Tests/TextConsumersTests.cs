namespace Sandbox.Tests;

public class TextConsumersTests
{
    [Fact]
    public void TextSubmitted_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new TextSubmitted(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Fact]
    public void TextStatsConsumer_records_words_lines_and_spaces()
    {
        var consumer = new TextStatsConsumer();
        consumer.Consume(new TextSubmitted("hello world\nagain"));
        Assert.Equal(new TextStats(3, 2, 1), consumer.Last);
    }

    [Theory]
    [InlineData("a{9}b", 2, 1, 1)]
    [InlineData("a{A0}b c", 3, 1, 2)]
    [InlineData("a{D}{A}b", 2, 2, 0)]
    [InlineData("a{2028}b{2029}c", 3, 3, 0)]
    public void TextStatsConsumer_counts_whitespace_other_than_line_terminators_as_spaces(
        string pattern, int words, int lines, int spaces)
    {
        // "{hex}" stands for the UTF-16 char with that code.
        var text = System.Text.RegularExpressions.Regex.Replace(
            pattern, "{([0-9A-F]+)}", m => ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString());
        var consumer = new TextStatsConsumer();
        consumer.Consume(new TextSubmitted(text));
        Assert.Equal(new TextStats(words, lines, spaces), consumer.Last);
    }

    [Fact]
    public void TextStatsConsumer_does_not_count_vertical_tab_or_form_feed_as_spaces()
    {
        var consumer = new TextStatsConsumer();
        consumer.Consume(new TextSubmitted("a\vb"));
        Assert.Equal(new TextStats(2, 2, 0), consumer.Last);

        consumer.Consume(new TextSubmitted("a\fb c"));
        Assert.Equal(new TextStats(3, 2, 1), consumer.Last);
    }

    [Fact]
    public void TextStatsConsumer_throws_on_null_message()
    {
        var consumer = new TextStatsConsumer();
        var ex = Assert.Throws<ArgumentNullException>(() => consumer.Consume(null!));
        Assert.Equal("message", ex.ParamName);
    }

    [Fact]
    public void TextFormatConsumer_capitalizes_text()
    {
        var consumer = new TextFormatConsumer();
        consumer.Consume(new TextSubmitted("hello world"));
        Assert.Equal("Hello World", consumer.Last);
    }

    [Fact]
    public void TextFormatConsumer_reverses_text_after_rev_prefix()
    {
        var consumer = new TextFormatConsumer();
        consumer.Consume(new TextSubmitted("REV:abc"));
        Assert.Equal("cba", consumer.Last);
    }

    [Fact]
    public void TextFormatConsumer_throws_on_null_message()
    {
        var consumer = new TextFormatConsumer();
        var ex = Assert.Throws<ArgumentNullException>(() => consumer.Consume(null!));
        Assert.Equal("message", ex.ParamName);
    }
}

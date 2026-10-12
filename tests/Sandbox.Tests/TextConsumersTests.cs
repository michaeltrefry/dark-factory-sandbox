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

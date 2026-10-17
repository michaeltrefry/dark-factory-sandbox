namespace Sandbox.Tests;

public class HotFileCountTests
{
    [Fact]
    public void Words_counts_a_b() =>
        Assert.Equal(2, HotFileCount.Words("a b"));
}

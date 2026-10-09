namespace DarkFactoryE2e;

// [dark-factory e2e] seeded by the acceptance test: it passes on the base, so the gate must reject it.
public class E2eAlreadyPassingTests
{
    [Xunit.Fact]
    public void Already_passes_on_the_base() => Xunit.Assert.True(true);
}

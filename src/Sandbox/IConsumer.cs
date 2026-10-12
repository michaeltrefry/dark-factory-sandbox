namespace Sandbox;

public interface IConsumer<in TMessage>
{
    void Consume(TMessage message);
}

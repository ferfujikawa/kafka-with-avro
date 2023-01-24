namespace KafkaWithAvro.Domain.MessageBus.Consumers
{
    public interface IConsumer<T>
    {
        Task ConsumeAsync(string topic, string group, Func<T, Task> onMessage, CancellationToken cancellation);
    }
}

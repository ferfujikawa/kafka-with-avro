namespace KafkaWithAvro.Domain.MessageBus.Producers
{
    public interface IProducer<T>
    {
        Task ProduceAsync(string topic, T message);
    }
}

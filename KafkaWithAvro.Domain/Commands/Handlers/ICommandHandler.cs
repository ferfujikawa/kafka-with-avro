namespace KafkaWithAvro.Domain.Commands.Handlers
{
    public interface ICommandHandler<T>
    {
        Task HandleAsync(T request);
    }
}

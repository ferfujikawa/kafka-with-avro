using KafkaWithAvro.Domain.MessageBus.Consumers.Dtos;
using KafkaWithAvro.Domain.MessageBus.Consumers;
using Microsoft.Extensions.Hosting;

namespace KafkaWithAvro.Consumer.App.Services
{
    public class ConsumeCreatedUserService : BackgroundService
    {
        private readonly IConsumer<UserDto> _consumer;

        public ConsumeCreatedUserService(IConsumer<UserDto> consumer)
        {
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.ConsumeAsync("created-user", "group1", OnMessage, stoppingToken);
        }

        private async Task OnMessage(UserDto message)
        {
            Console.WriteLine($"User - Id: {message.Id}, Name: {message.Name}, Email: {message.Email}");
            await Task.CompletedTask;
        }
    }
}

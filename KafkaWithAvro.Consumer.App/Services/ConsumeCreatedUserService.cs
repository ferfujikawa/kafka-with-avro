using KafkaWithAvro.Domain.MessageBus.Consumers.Dtos;
using KafkaWithAvro.Domain.MessageBus.Consumers;
using Microsoft.Extensions.Hosting;
using KafkaWithAvro.Consumer.App.Settings;

namespace KafkaWithAvro.Consumer.App.Services
{
    public class ConsumeCreatedUserService : BackgroundService
    {
        private readonly IConsumer<UserDto> _consumer;
        private readonly ConsumerSettings _consumerSetting;

        public ConsumeCreatedUserService(
            IConsumer<UserDto> consumer,
            ConsumerSettings consumerSetting)
        {
            _consumer = consumer;
            _consumerSetting = consumerSetting;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.ConsumeAsync(
                _consumerSetting.Topic,
                _consumerSetting.Topic,
                OnMessage,
                stoppingToken);
        }

        private async Task OnMessage(UserDto message)
        {
            Console.WriteLine($"User - Id: {message.Id}, Name: {message.Name}, Email: {message.Email}");
            await Task.CompletedTask;
        }
    }
}

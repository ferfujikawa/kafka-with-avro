using Confluent.Kafka;
using Confluent.SchemaRegistry;
using KafkaWithAvro.Consumer.App.Services;
using KafkaWithAvro.Domain.MessageBus.Consumers.Dtos;
using KafkaWithAvro.Domain.MessageBus.Consumers;
using KafkaWithAvro.Infra.Kafka.Configurations;
using KafkaWithAvro.Infra.Kafka.Consumers;
using KafkaWithAvro.Infra.Kafka.SchemaRegistry.Client;
using KafkaWithAvro.Infra.Kafka.SchemaRegistry.Deserializers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using KafkaWithAvro.Consumer.App.Settings;

namespace KafkaWithAvro.Consumer.App.Extensions
{
    public static class AppExtensions
    {
        public static void AddKafkaSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var kafkaConfig = new KafkaConfig();
            configuration
                .GetSection("KafkaSettings")
                .Bind(kafkaConfig);
            services.AddSingleton(kafkaConfig);
            services.AddSingleton<ISchemaRegistryClient>(x => new SchemaRegistryClient(kafkaConfig.SchemaServer));
        }

        public static void AddConsumerSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var consumerSetting = new ConsumerSettings();
            configuration
                .GetSection("ConsumerSettings")
                .Bind(consumerSetting);
            services.AddSingleton(consumerSetting);
        }

        public static void AddKafkaComponents(this IServiceCollection services)
        {
            services.AddSingleton<IAsyncDeserializer<UserDto>, CreateUserDeserializer>();
            services.AddSingleton(typeof(IConsumer<>), typeof(Consumer<>));
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ConsumeCreatedUserService>();
        }

        public static async Task StartServicesAsync(this IServiceProvider services, IHostApplicationLifetime lifetime)
        {
            var service = services.GetRequiredService<ConsumeCreatedUserService>();
            await service.StartAsync(lifetime.ApplicationStopping);
        }
    }
}

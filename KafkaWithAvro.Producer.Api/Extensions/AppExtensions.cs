using Confluent.Kafka;
using Confluent.SchemaRegistry;
using KafkaWithAvro.Domain.Commands.Handlers;
using KafkaWithAvro.Domain.Entities;
using KafkaWithAvro.Domain.MessageBus.Producers;
using KafkaWithAvro.Infra.Kafka.Configurations;
using KafkaWithAvro.Infra.Kafka.Producers;
using KafkaWithAvro.Infra.Kafka.SchemaRegistry.Client;
using KafkaWithAvro.Infra.Kafka.SchemaRegistry.Serializers;

namespace KafkaWithAvro.Producer.Api.Extensions
{
    public static class AppExtensions
    {
        public static void AddKafkaSettings(this WebApplicationBuilder builder)
        {
            var kafkaConfig = new KafkaConfig();
            builder.Configuration.GetSection("KafkaSettings").Bind(kafkaConfig);
            builder.Services.AddSingleton(x => kafkaConfig);
            builder.Services.AddSingleton<ISchemaRegistryClient>(x => new SchemaRegistryClient(kafkaConfig.SchemaServer));
        }

        public static void AddKafkaComponents(this IServiceCollection services)
        {
            services.AddSingleton<IAsyncSerializer<User>, CreateUserSerializer>();
            services.AddSingleton(typeof(IProducer<>), typeof(Producer<>));
        }

        public static void AddHandlers(this IServiceCollection services)
        {
            services.AddTransient<CreateUserHandler>();
        }
    }
}

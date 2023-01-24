using Confluent.SchemaRegistry;
using KafkaWithAvro.Domain.MessageBus.Producers;
using KafkaWithAvro.Infra.Kafka.Configurations;
using KafkaWithAvro.Infra.Kafka.Producers;
using KafkaWithAvro.Infra.Kafka.SchemaRegistry.Client;

namespace KafkaWithAvro.Producer.Api.Extensions
{
    public static class AppExtensions
    {
        public static void AddKafkaSettings(this WebApplicationBuilder builder)
        {
            var kafkaConfig = new KafkaConfig();
            builder.Configuration.GetSection("KafkaSettings").Bind(kafkaConfig);
            builder.Services.AddSingleton(x => kafkaConfig);
        }

        public static void AddKafkaComponents(this IServiceCollection services)
        {
            var kafkaConfig = services.BuildServiceProvider().GetService<KafkaConfig>();
            if (kafkaConfig != null)
            {
                services.AddSingleton<ISchemaRegistryClient>(x => new SchemaRegistryClient(kafkaConfig.SchemaServer));
            }
            
            services.AddSingleton(typeof(IProducer<>), typeof(Producer<>));
        }
    }
}

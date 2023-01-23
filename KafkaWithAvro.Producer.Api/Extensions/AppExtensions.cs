using KafkaWithAvro.Domain.MessageBus.Producers;
using KafkaWithAvro.Infra.Kafka.Configurations;
using KafkaWithAvro.Infra.Kafka.Producers;

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
            services.AddSingleton(typeof(IProducer<>), typeof(Producer<>));
        }
    }
}

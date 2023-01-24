using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using KafkaWithAvro.Domain.MessageBus.Consumers;
using KafkaWithAvro.Infra.Kafka.Configurations;

namespace KafkaWithAvro.Infra.Kafka.Consumers
{
    public class Consumer<T> : IConsumer<T>
    {
        private readonly KafkaConfig _kafkaConfig;
        private readonly IAsyncDeserializer<T> _deserializer;

        public Consumer(
            KafkaConfig kafkaConfig,
            IAsyncDeserializer<T> deserializer)
        {
            _kafkaConfig = kafkaConfig;
            _deserializer = deserializer;
        }

        public async Task ConsumeAsync(string topic, string group, Func<T, Task> onMessage, CancellationToken cancellation)
        {
            _ = Task.Factory.StartNew(async () =>
            {
                var config = CreateConfig(group);
                var consumer = new ConsumerBuilder<string, T>(config)
                    .SetValueDeserializer(_deserializer.AsSyncOverAsync())
                    .Build();
                consumer.Subscribe(topic);

                while (!cancellation.IsCancellationRequested)
                {
                    var result = consumer.Consume();

                    if (result.IsPartitionEOF)
                        continue;

                    await onMessage(result.Message.Value);
                }
            }, cancellation, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            await Task.CompletedTask;
        }

        private ConsumerConfig CreateConfig(string group)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _kafkaConfig.BootstrapServer,
                GroupId = group
            };
            return config;
        }
    }
}

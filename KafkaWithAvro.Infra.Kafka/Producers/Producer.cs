using Confluent.Kafka;
using KafkaWithAvro.Domain.MessageBus.Producers;
using KafkaWithAvro.Infra.Kafka.Configurations;

namespace KafkaWithAvro.Infra.Kafka.Producers
{
    public class Producer<T> : IProducer<T>
    {
        private readonly KafkaConfig _kafkaConfig;

        public IAsyncSerializer<T> _serializer { get; }

        public Producer(
            KafkaConfig kafkaConfig,
            IAsyncSerializer<T> serializer)
        {
            _kafkaConfig = kafkaConfig;
            _serializer = serializer;
        }

        public async Task ProduceAsync(string topic, T message)
        {
            var config = CreateConfig();

            var producer = new ProducerBuilder<string, T>(config)
                .SetValueSerializer(_serializer)
                .Build();

            //Criar uma transação
            producer.InitTransactions(TimeSpan.FromSeconds(5));

            try
            {
                //Iniciar a transação
                producer.BeginTransaction();

                var payload = new Message<string, T>
                {
                    Key = Guid.NewGuid().ToString(),
                    Value = message
                };
                var result = await producer.ProduceAsync(topic, payload);

                //Confirmar a transação
                producer.CommitTransaction();
            }
            catch
            {
                //Abortar a transação
                producer.AbortTransaction();
            }

            await Task.CompletedTask;
        }

        private ProducerConfig CreateConfig()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _kafkaConfig.BootstrapServer,

                #region Habilitar idempotência
                EnableIdempotence = true,
                Acks = Acks.All,
                MaxInFlight = 1,
                MessageSendMaxRetries = 2,
                #endregion

                //Habilitar transação
                TransactionalId = Guid.NewGuid().ToString()
            };
            return config;
        }
    }
}

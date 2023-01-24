using Confluent.SchemaRegistry;

namespace KafkaWithAvro.Infra.Kafka.SchemaRegistry.Client
{
    public class SchemaRegistryClient : CachedSchemaRegistryClient, ISchemaRegistryClient
    {
        public SchemaRegistryClient(string schemaRegistryUrl) : base(new SchemaRegistryConfig { Url = schemaRegistryUrl })
        {
        }
    }
}

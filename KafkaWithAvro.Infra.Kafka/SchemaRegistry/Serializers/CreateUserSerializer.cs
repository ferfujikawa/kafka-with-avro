using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using KafkaWithAvro.Domain.Entities;
using KafkaWithAvro.Infra.Kafka.SchemaRegistry.Avros.Models;

namespace KafkaWithAvro.Infra.Kafka.SchemaRegistry.Serializers
{
    public class CreateUserSerializer : IAsyncSerializer<User>
    {
        private readonly IAsyncSerializer<CreatedUser> _serializer;

        public CreateUserSerializer(ISchemaRegistryClient schemaRegistryClient)
        {
            _serializer = new AvroSerializer<CreatedUser>(schemaRegistryClient);
        }

        public async Task<byte[]> SerializeAsync(User data, SerializationContext context)
        {
            var avroObject = new CreatedUser
            {
                Id = data.Id.ToString(),
                Name = data.Name,
                Email = data.Email
            };

            return await _serializer.SerializeAsync(avroObject, context);
        }
    }
}

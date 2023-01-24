using Confluent.Kafka;
using Confluent.SchemaRegistry.Serdes;
using Confluent.SchemaRegistry;
using KafkaWithAvro.Infra.Kafka.SchemaRegistry.Avros.Models;
using KafkaWithAvro.Domain.MessageBus.Consumers.Dtos;

namespace KafkaWithAvro.Infra.Kafka.SchemaRegistry.Deserializers
{
    public class CreateUserDeserializer : IAsyncDeserializer<UserDto>
    {
        private readonly IAsyncDeserializer<CreatedUser> _deserializer;

        public CreateUserDeserializer(ISchemaRegistryClient schemaRegistryClient)
        {
            _deserializer = new AvroDeserializer<CreatedUser>(schemaRegistryClient);
        }

        public async Task<UserDto> DeserializeAsync(ReadOnlyMemory<byte> data, bool isNull, SerializationContext context)
        {
            var createdUser = await _deserializer.DeserializeAsync(data, isNull, context);
            return new UserDto {
                Id = Guid.Parse(createdUser.Id),
                Name = createdUser.Name,
                Email = createdUser.Email
            };
        }
    }
}

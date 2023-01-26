using Confluent.Kafka;
using Confluent.SchemaRegistry;
using KafkaWithAvro.Consumer.App.Services;
using KafkaWithAvro.Domain.MessageBus.Consumers;
using KafkaWithAvro.Domain.MessageBus.Consumers.Dtos;
using KafkaWithAvro.Infra.Kafka.Configurations;
using KafkaWithAvro.Infra.Kafka.Consumers;
using KafkaWithAvro.Infra.Kafka.SchemaRegistry.Client;
using KafkaWithAvro.Infra.Kafka.SchemaRegistry.Deserializers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

builder.ConfigureServices(services =>
{
    var kafkaConfig = new KafkaConfig();
    config.GetSection("KafkaSettings").Bind(kafkaConfig);
    services.AddSingleton(x => kafkaConfig);
    services.AddSingleton<ISchemaRegistryClient>(x => new SchemaRegistryClient(kafkaConfig.SchemaServer));
    services.AddSingleton<IAsyncDeserializer<UserDto>, CreateUserDeserializer>();

    services.AddSingleton(typeof(IConsumer<>), typeof(Consumer<>));

    services.AddSingleton<ConsumeCreatedUserService>();
});

using (var host = builder.Build())
{
    await host.StartAsync();
    var lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();

    var service = host.Services.GetRequiredService<ConsumeCreatedUserService>();
    await service.StartAsync(lifetime.ApplicationStopping);

    await host.WaitForShutdownAsync(lifetime.ApplicationStopping);
}
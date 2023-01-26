using KafkaWithAvro.Consumer.App.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

builder.ConfigureServices(services =>
{
    services.AddKafkaSettings(config);
    services.AddKafkaComponents();
    services.AddServices();
});

using (var host = builder.Build())
{
    await host.StartAsync();
    var lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();

    await host.Services.StartServicesAsync(lifetime);

    await host.WaitForShutdownAsync(lifetime.ApplicationStopping);
}
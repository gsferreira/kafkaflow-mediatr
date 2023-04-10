using KafkaFlow;
using KafkaFlow.MediatR;
using KafkaFlow.MediatR.Samples.Notifications;
using KafkaFlow.Producers;
using KafkaFlow.Serializer;
using Microsoft.Extensions.DependencyInjection;

const string topicName = "sample";
const string producerName = "say-hello";

var services = new ServiceCollection();

services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
});

services.AddKafka(kafka => kafka
    .UseConsoleLog()
    .AddCluster(cluster => cluster
        .WithBrokers(new[] { "localhost:9092" })
        .CreateTopicIfNotExists(topicName, 1, 1)
        .AddConsumer(consumer => consumer
            .Topic(topicName)
            .WithGroupId("sample")
            .WithName("sample-consumer")
            .WithBufferSize(100)
            .WithWorkersCount(10)
            .AddMiddlewares(middlewares => middlewares
                .AddSerializer<JsonCoreSerializer>()
                .AddMediatorNotifications(MiddlewareLifetime.Transient)
            )
        )
        .AddProducer(
            producerName,
            producer => producer
                .DefaultTopic(topicName)
                .AddMiddlewares(m =>
                    m.AddSerializer<JsonCoreSerializer>()
                )
        )
    )
);

var serviceProvider = services.BuildServiceProvider();

var producer = serviceProvider
    .GetRequiredService<IProducerAccessor>()
    .GetProducer(producerName);

var bus = serviceProvider.CreateKafkaBus();

await bus.StartAsync();

string? message;
do
{
    Console.WriteLine("What message you want to send?");
    message = Console.ReadLine();

    if (!string.IsNullOrEmpty(message))
        await producer.ProduceAsync(
            topicName,
            Guid.NewGuid().ToString(),
            new HelloMessage { Text = message });
    
} while (!string.IsNullOrEmpty(message));

await bus.StopAsync();
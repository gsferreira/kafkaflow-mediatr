# KafkaFlow.MediatR

## Introduction

An Extension to [KafkaFlow](https://github.com/farfetch/kafkaflow) that adds [MediatR](https://github.com/jbogard/MediatR) as a Middleware.

You can find samples [here](https://github.com/gsferreira/kafkaflow-mediatr/tree/main/samples/).

## Installation

Using .NET CLI:

```bash
dotnet add package gsferreira.KafkaFlow.MediatR
```

Using [NuGet](https://www.nuget.org/packages/gsferreira.KafkaFlow.MediatR):

```bash
Install-Package gsferreira.KafkaFlow.MediatR
```

## Documentation

You can forward messages as Requests or Notifications to Mediator.
To do it, chain one of the middlewares provided by this library after a Serializer Middleware. 
The serializer should be able to deserialize the incoming message into a message that implements MediatR `IRequest` or `INotification`.

### Publishing Notifications

To publish messages as notifications to Mediator, use the middleware `AddMediatorNotifications()`.

```csharp
services.AddKafka(kafka =>
    kafka
        .AddCluster(cluster => cluster
            .WithBrokers(new[] { "localhost:9092" })
            .AddConsumer(consumer => consumer
                .Topic("sample-topic")
                .WithGroupId("sample-group")
                .WithBufferSize(100)
                .WithWorkersCount(10)
                .AddMiddlewares(middlewares => middlewares
                    .AddSerializer<JsonCoreSerializer>()
                    .AddMediatorNotifications()
                )
            )
        ));
```

### Publishing Requests

To publish messages as requests to Mediator, use the middleware `AddMediatorRequests()`.

```csharp
services.AddKafka(kafka =>
    kafka
        .AddCluster(cluster => cluster
            .WithBrokers(new[] { "localhost:9092" })
            .AddConsumer(consumer => consumer
                .Topic("sample-topic")
                .WithGroupId("sample-group")
                .WithBufferSize(100)
                .WithWorkersCount(10)
                .AddMiddlewares(middlewares => middlewares
                    .AddSerializer<JsonCoreSerializer>()
                    .AddMediatorRequests()
                )
            )
        ));
```

### Controlling lifetime

The default KafkaFlow Middleware lifetime isn't compatible with the default MediatR Handler lifetime.
You need to align those either through the Middleware overload `AddMediatorNotifications(lifetime)`/`AddMediatorNotifications(lifetime)` or through MediatR defaults configuration: 

```csharp
services.AddMediatR(cfg =>
{
    cfg.Lifetime = ServiceLifetime.Singleton;
});
```

## Get in touch

-   [GitHub Issues](https://github.com/gsferreira/kafkaflow-mediatr/issues)

## License

KafkaFlow.MediatR is a free and open source project, released under the permissible [MIT license](LICENSE).

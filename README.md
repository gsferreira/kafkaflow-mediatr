# KafkaFlow.MediatR

## Introduction

An Extension to [KafkaFlow](https://github.com/farfetch/kafkaflow) that adds [MediatR](https://github.com/jbogard/MediatR) as a Middleware.

## Installation

```bash
dotnet add package gsferreira.KafkaFlow.MediatR
```

## Documentation

How to use it:

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
                    // To Publish messages as Notifications
                    middlewares.AddMediatorNotifications(),
                    // To Send messages as Requests
                    middlewares.AddMediatorRequests()
                )
            )
        ));

```

## Get in touch

-   [GitHub Issues](https://github.com/gsferreira/kafkaflow-mediatr/issues)

## License

KafkaFlow.MediatR is a free and open source project, released under the permissible [MIT license](LICENSE).

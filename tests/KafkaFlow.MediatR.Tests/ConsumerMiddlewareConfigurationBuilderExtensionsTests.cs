using FluentAssertions;
using KafkaFlow.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaFlow.MediatR.Tests;

public class ConsumerMiddlewareConfigurationBuilderExtensionsTests
{
    private readonly ServiceCollection _services;

    public ConsumerMiddlewareConfigurationBuilderExtensionsTests()
    {
        _services = new ServiceCollection();
        _services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(typeof(ConsumerMiddlewareConfigurationBuilderExtensionsTests)
                .Assembly));
    }

    [Fact]
    public void WhenAddMediatorNotifications_ThenIsAddedOnceToRegistrationServices()
    {
        AddKafkaConfiguration((
                middlewares) =>
            middlewares.AddMediatorNotifications());

        AssertMiddlewareHasRegistered(typeof(MediatorNotificationMiddleware));
    }

    [Fact]
    public void WhenAddMediatorRequests_ThenIsAddedOnceToRegistrationServices()
    {
        AddKafkaConfiguration((
                middlewares) =>
            middlewares.AddMediatorRequests());

        AssertMiddlewareHasRegistered(typeof(MediatorRequestMiddleware));
    }

    [Fact]
    public void WhenAddMediatorNotificationsWithLifetime_ThenIsAddedOnceToRegistrationServices()
    {
        AddKafkaConfiguration((
                middlewares) =>
            middlewares.AddMediatorNotifications(MiddlewareLifetime.Singleton));

        AssertMiddlewareHasRegistered(typeof(MediatorNotificationMiddleware), ServiceLifetime.Singleton);
    }

    [Fact]
    public void WhenAddMediatorRequestsWithLifetime_ThenIsAddedOnceToRegistrationServices()
    {
        AddKafkaConfiguration((
                middlewares) =>
            middlewares.AddMediatorRequests(MiddlewareLifetime.Singleton));

        AssertMiddlewareHasRegistered(typeof(MediatorRequestMiddleware), ServiceLifetime.Singleton);
    }

    private void AssertMiddlewareHasRegistered(Type expectedType, ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        var descriptors =
            _services.Where(x => x.ServiceType == expectedType).ToArray();

        descriptors.Should().HaveCount(1);
        descriptors.First().Lifetime.Should().Be(lifetime);
    }

    private void AddKafkaConfiguration(Action<IConsumerMiddlewareConfigurationBuilder> action)
    {
        _services.AddKafka(kafka =>
            kafka
                .AddCluster(cluster => cluster
                    .WithBrokers(new[] { "localhost:9092" })
                    .AddConsumer(consumer => consumer
                        .Topic("sample-topic")
                        .WithGroupId("sample-group")
                        .WithBufferSize(100)
                        .WithWorkersCount(10)
                        .AddMiddlewares(action)
                    )
                ));
    }
}
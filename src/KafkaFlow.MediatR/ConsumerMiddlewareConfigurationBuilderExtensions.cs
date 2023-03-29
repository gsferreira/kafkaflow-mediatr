using KafkaFlow.Configuration;

namespace KafkaFlow.MediatR;

public static class ConsumerMiddlewareConfigurationBuilderExtensions
{
    public static IConsumerMiddlewareConfigurationBuilder AddMediatorNotifications(
        this IConsumerMiddlewareConfigurationBuilder builder)
    {
        builder.Add<MediatorNotificationMiddleware>();

        return builder;
    }

    public static IConsumerMiddlewareConfigurationBuilder AddMediatorNotifications(
        this IConsumerMiddlewareConfigurationBuilder builder, MiddlewareLifetime middlewareLifetime)
    {
        builder.Add<MediatorNotificationMiddleware>(middlewareLifetime);

        return builder;
    }

    public static IConsumerMiddlewareConfigurationBuilder AddMediatorRequests(
        this IConsumerMiddlewareConfigurationBuilder builder)
    {
        builder.Add<MediatorRequestMiddleware>();

        return builder;
    }

    public static IConsumerMiddlewareConfigurationBuilder AddMediatorRequests(
        this IConsumerMiddlewareConfigurationBuilder builder, MiddlewareLifetime middlewareLifetime)
    {
        builder.Add<MediatorRequestMiddleware>(middlewareLifetime);

        return builder;
    }
}
using MediatR;

namespace KafkaFlow.MediatR.Samples.Notifications.Handlers;

public class HelloMessageHandler : INotificationHandler<HelloMessage>
{
    public Task Handle(HelloMessage notification, CancellationToken cancellationToken)
    {
        Console.WriteLine(
            $"Notification received: {notification.Text}");
        return Task.CompletedTask;
    }
}
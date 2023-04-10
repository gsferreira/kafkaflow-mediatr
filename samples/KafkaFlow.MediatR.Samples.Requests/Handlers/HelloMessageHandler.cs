using MediatR;

namespace KafkaFlow.MediatR.Samples.Requests.Handlers;

public class HelloMessageHandler : IRequestHandler<HelloMessage>
{
    public Task Handle(HelloMessage notification, CancellationToken cancellationToken)
    {
        Console.WriteLine(
            $"Request received: {notification.Text}");
        return Task.CompletedTask;
    }
}
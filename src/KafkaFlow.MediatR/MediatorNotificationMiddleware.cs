using MediatR;

namespace KafkaFlow.MediatR;

public class MediatorNotificationMiddleware : MediatorBaseMiddleware
{
    public MediatorNotificationMiddleware(IMediator mediator)
        : base(mediator)
    {
    }

    protected override Task InvokeMediator(object message, CancellationToken cancellationToken)
    {
        return Mediator.Publish(message, cancellationToken);
    }
}
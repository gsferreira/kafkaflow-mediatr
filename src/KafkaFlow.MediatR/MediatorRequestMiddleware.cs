using MediatR;

namespace KafkaFlow.MediatR;

public class MediatorRequestMiddleware : MediatorBaseMiddleware
{
    public MediatorRequestMiddleware(IMediator mediator) : base(mediator)
    {
    }

    protected override Task InvokeMediator(object message, CancellationToken cancellationToken)
    {
        return Mediator.Send(message, cancellationToken);
    }
}
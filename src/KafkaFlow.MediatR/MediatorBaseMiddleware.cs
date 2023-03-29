using MediatR;

namespace KafkaFlow.MediatR;

public abstract class MediatorBaseMiddleware : IMessageMiddleware
{
    protected readonly IMediator Mediator;

    protected MediatorBaseMiddleware(IMediator mediator)
    {
        Mediator = mediator;
    }

    public async Task Invoke(IMessageContext context, MiddlewareDelegate next)
    {
        await InvokeMediator(context.Message.Value, context.ConsumerContext.WorkerStopped);

        await next(context)!;
    }

    protected abstract Task InvokeMediator(object message, CancellationToken cancellationToken);
}
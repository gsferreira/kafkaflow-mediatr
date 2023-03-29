using MediatR;
using NSubstitute;

namespace KafkaFlow.MediatR.Tests;

public class MediatorNotificationTests
{
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly IMessageContext _context = Substitute.For<IMessageContext>();
    private readonly MiddlewareDelegate _next = Substitute.For<MiddlewareDelegate>();
    private readonly MediatorNotificationMiddleware _middleware;

    public MediatorNotificationTests()
    {
        _middleware = new MediatorNotificationMiddleware(_mediator);
    }

    [Fact]
    public async Task WhenHandleMessage_ThenPublishNotificationThroughMediator()
    {
        await _middleware.Invoke(_context, _next);

        await _mediator.Received(1).Publish(_context.Message.Value, default);
    }

    [Fact]
    public async Task WhenPublishNotification_ThenCallNextMiddleware()
    {
        await _middleware.Invoke(_context, _next);

        await _next.Received(1).Invoke(_context);
    }
}
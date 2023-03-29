using MediatR;
using NSubstitute;

namespace KafkaFlow.MediatR.Tests;

public class MediatorRequestTests
{
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly IMessageContext _context = Substitute.For<IMessageContext>();
    private readonly MiddlewareDelegate _next = Substitute.For<MiddlewareDelegate>();
    private readonly MediatorRequestMiddleware _middleware;

    public MediatorRequestTests()
    {
        _middleware = new MediatorRequestMiddleware(_mediator);
    }

    [Fact]
    public async Task WhenHandleMessage_ThenSendRequestThroughMediator()
    {
        await _middleware.Invoke(_context, _next);

        await _mediator.Received(1).Send(_context.Message.Value, default);
    }

    [Fact]
    public async Task WhenSendRequest_ThenCallNextMiddleware()
    {
        await _middleware.Invoke(_context, _next);

        await _next.Received(1).Invoke(_context);
    }
}
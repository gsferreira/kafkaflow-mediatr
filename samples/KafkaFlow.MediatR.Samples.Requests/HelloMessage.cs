using MediatR;

namespace KafkaFlow.MediatR.Samples.Requests;

public class HelloMessage : IRequest
{
    public string? Text { get; set; } = default!;
}
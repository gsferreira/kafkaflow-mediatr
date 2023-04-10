using MediatR;

namespace KafkaFlow.MediatR.Samples.Notifications;

public class HelloMessage : INotification
{
    public string? Text { get; set; } = default!;
}
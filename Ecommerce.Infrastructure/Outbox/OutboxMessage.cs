namespace Ecommerce.Infrastructure.Outbox
{
    public class OutboxMessage
    {
        public Guid Id { get; set; }
        public string EventName { get; set; } = "";
        public string Data { get; set; } = "";
        public string CorrelationId { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
    }
}

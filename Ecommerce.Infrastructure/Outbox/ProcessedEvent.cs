namespace Ecommerce.Infrastructure.Outbox
{
    public class ProcessedEvent
    {
        public Guid Id { get; set; }
        public Guid MessageId { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}

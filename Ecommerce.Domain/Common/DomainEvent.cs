namespace Ecommerce.Domain.Common
{
    public abstract record DomainEvent
    {
        public DateTime OccuredOn { get; init; } = DateTime.UtcNow;
    }
}

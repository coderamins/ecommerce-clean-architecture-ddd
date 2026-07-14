namespace Ecommerce.Application.Common.Interfaces
{
    public interface IRequestContext
    {
        string CorrelationId { get; }
        string? UserId { get; }
        string? IpAddress { get; }
    }
}

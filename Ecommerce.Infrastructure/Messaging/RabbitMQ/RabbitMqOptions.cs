namespace Ecommerce.Infrastructure.Messaging.RabbitMQ
{
    public sealed class RabbitMqOptions
    {
        public const string SectionName = "RabbitMQ";
        public string Host { get; init; } = default!;
        public int Port { get; init; }
        public string UserName { get; init; } = default!;
        public string Password { get; init; } = default!;
    }
}

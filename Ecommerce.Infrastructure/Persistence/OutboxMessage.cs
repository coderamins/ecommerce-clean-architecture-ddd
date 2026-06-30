using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Infrastructure.Persistence
{
    public class OutboxMessage
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = "";
        public string Data { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
    }
}

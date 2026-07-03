using Ecommerce.Domain.Orders;
using Ecommerce.Infrastructure.Outbox;
using Ecommerce.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> Items => Set<OrderItem>();
        public DbSet<OutboxMessage> Outbox => Set<OutboxMessage>();
        public DbSet<ProcessedEvent> ProcessedEvents => Set<ProcessedEvent>();  

        #region Read models
        public DbSet<OrderReadModel> OrderReads => Set<OrderReadModel>();

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderReadModel>()
                .HasKey(x => x.Id);

            builder.Entity<OrderReadModel>()
             .OwnsMany(x => x.Items);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}

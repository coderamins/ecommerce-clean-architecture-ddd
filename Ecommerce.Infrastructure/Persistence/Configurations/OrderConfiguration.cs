using Ecommerce.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration
        : IEntityTypeConfiguration<Order>
    {
        public void Configure(
            EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .OwnsOne(
                    x => x.Total,
                    money =>
                    {
                        money
                            .Property(
                                x => x.Amount
                            )
                            .HasColumnName(
                                "Total"
                            );
                    });

            builder
                .OwnsMany(
                    x => x.Items,
                    item =>
                    {
                        item.WithOwner();

                        item.HasKey(
                            x => x.Id
                        );

                        item
                            .OwnsOne(
                                x => x.Price,
                                money =>
                                {
                                    money
                                        .Property(
                                            x => x.Amount
                                        )
                                        .HasColumnName(
                                            "Price"
                                        );
                                });
                    });

            builder
                .Metadata
                .FindNavigation(
                    nameof(Order.Items)
                )!
                .SetPropertyAccessMode(
                    PropertyAccessMode.Field
                );
        }
    }
}

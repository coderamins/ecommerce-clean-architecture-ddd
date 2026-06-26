using Ecommerce.Domain.Common;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Domain.Entities;

public class Order : Entity
{
    private readonly List<OrderItem> _items = new();

    public IReadOnlyCollection<OrderItem> Items
        => _items;

    public Money Total { get; private set; }
        = Money.Zero;

    public bool IsPaid { get; private set; }

    private Order() { }
    public static Order Create()
    {
        return new Order();
    }

    public void AddItem(
        string product,
        int qty,
        Money price)
    {
        EnsureEditable();

        _items.Add(
            new OrderItem(
                product,
                qty,
                price
            )
        );

        Recalculate();
    }

    public void Pay()
    {
        if (!_items.Any())
            throw new Exception(
                "Order empty"
            );

        IsPaid = true;
    }

    private void Recalculate()
    {
        Total =
            _items
                .Select(x => x.Total())
                .Aggregate(
                    Money.Zero,
                    (a, b) => a + b
                );
    }

    private void EnsureEditable()
    {
        if (IsPaid)
            throw new Exception(
                "Order already paid"
            );
    }
}
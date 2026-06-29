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

    public Result AddItem(
        string product,
        int qty,
        Money price)
    {
        var editable =
       EnsureEditable();

        if (!editable.IsSuccess)
            return editable;
        if (price.Amount <= 0)
        {
            return Result.Failure(
                "Price invalid"
            );
        }

        _items.Add(
            new OrderItem(
                product,
                qty,
                price
            )
        );

        Recalculate();

        return Result
                .Success();
    }

    public Result Pay()
    {
        if (!_items.Any())
            return Result
            .Failure(
                "Order is empty"
            );

        IsPaid = true;

        return Result.Success();
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

    private Result EnsureEditable()
    {
        if (IsPaid)
            return Result.Failure("Order already paid");

        return Result
        .Success();
    }
}
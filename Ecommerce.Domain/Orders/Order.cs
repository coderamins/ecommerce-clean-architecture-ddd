using Ecommerce.Domain.Common;
using Ecommerce.Domain.Events;
using Ecommerce.Domain.Orders.Events;
using Ecommerce.Domain.Orders.ValueObjects;

namespace Ecommerce.Domain.Orders;

public class Order : AggregateRoot
{
    private readonly List<OrderItem> _items = new();

    public IReadOnlyCollection<OrderItem> Items => _items;

    public Money Total { get; private set; } = Money.Zero;

    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    private Order() { }
    public static Order Create()
    {
        return new Order();
    }

    public void CompleteCreation()
    {
        AddEvent(
            new OrderCreated(
                Id,
                Total.Amount,
                Items.Select(x => new OrderCreatedItem(
                    x.ProductName,
                    x.Quantity,
                    x.Price.Amount))
                .ToList()));
    }

    public Result AddItem(string product, int qty, Money price)
    {
        var editable = EnsureEditable();

        if (!editable.IsSuccess)
            return editable;

        if (price.Amount <= 0)
        {
            return Result.Failure("Price invalid");
        }

        _items.Add(new OrderItem(
                product,
                qty,
                price
            )
        );

        Recalculate();

        return Result.Success();
    }

    public Result Pay()
    {
        if (Status == OrderStatus.Cancelled)
        {
            return Result.Failure("Cancelled order cannot be paid");
        }

        if (Status == OrderStatus.Paid)
        {
            return Result.Failure("Order already paid");
        }

        if (Status == OrderStatus.Delivered)
        {
            return Result.Failure("Dlivered order cannot be paid");
        }

        if (!_items.Any())
            return Result.Failure("Order is empty");

        Status = OrderStatus.Paid;

        AddEvent(new OrderPaid(Id, DateTime.UtcNow));

        return Result.Success();
    }

    private void Recalculate()
    {
        Total =
            _items.Select(x => x.Total())
                .Aggregate(
                    Money.Zero,
                    (a, b) => a + b
                );

    }

    private Result EnsureEditable()
    {
        if (Status == OrderStatus.Paid)
            return Result.Failure("Order already paid");

        return Result
        .Success();
    }

    public Result Cancel()
    {
        if (Status == OrderStatus.Cancelled)
            return Result.Failure("Order laready cancelled");

        if (Status == OrderStatus.Paid)
            return Result.Failure("Paid order cannot be cancelled");

        if (Status == OrderStatus.Shipped)
            return Result.Failure("Shipped order cannot be cancelled");

        if (Status == OrderStatus.Delivered)
            return Result.Failure("Delivered order cannot be cancelled");

        Status = OrderStatus.Cancelled;

        AddEvent(new OrderCancelled(Id, DateTime.UtcNow));

        return Result.Success();
    }
}
using Ecommerce.Application.Common.Abstractions;

namespace Ecommerce.Application.Features.Orders.Create
{
    public sealed record CreateOrderCommand(
        IReadOnlyCollection<CreateOrderItem> Items)
        : ICommand<Guid>;

}

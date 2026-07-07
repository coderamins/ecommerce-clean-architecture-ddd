using MediatR;

namespace Ecommerce.Application.Features.Orders.Create
{
    public sealed record CreateOrderCommand(
        IReadOnlyCollection<CreateOrderItem> Items)
        : IRequest<Guid>;

}

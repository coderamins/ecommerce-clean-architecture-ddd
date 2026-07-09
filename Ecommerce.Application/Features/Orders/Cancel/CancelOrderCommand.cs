using Ecommerce.Application.Common.Abstractions;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Cancel
{
    public sealed record CancelOrderCommand(Guid OrderId)
    : ICommand<Unit>;
}

using MediatR;

namespace Ecommerce.Application.Features.Orders.Cancel
{
    public record CancelOrderCommand(Guid OrderId):IRequest;
}

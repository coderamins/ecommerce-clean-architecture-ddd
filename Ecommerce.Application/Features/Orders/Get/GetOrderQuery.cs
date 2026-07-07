using MediatR;

namespace Ecommerce.Application.Features.Orders.Get
{
    public record GetOrderQuery(Guid OrderId) : IRequest<GetOrderResponse>;
}

using MediatR;

namespace Ecommerce.Application.Features.Orders.Pay
{
    public record PayOrderCommand(Guid orderId) : IRequest;
}

using Ecommerce.Application.Common.Abstractions;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Get
{
    public record GetOrderQuery(Guid OrderId) : IQuery<GetOrderResponse>;
}

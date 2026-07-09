using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Common;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Get
{
    public class GetOrderHandler:IRequestHandler<GetOrderQuery, GetOrderResponse>
    {
        private readonly IOrderReadRepository _repository;

        public GetOrderHandler(IOrderReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetOrderResponse> Handle(GetOrderQuery request,CancellationToken ct)
        {
            return await _repository
                .GetByIdAsync(request.OrderId) ?? 
                    throw new DomainException("Order not fount.");

        }
    }
}

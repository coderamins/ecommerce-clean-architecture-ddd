using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Orders.Repositories;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Get
{
    public class GetOrderHandler:IRequestHandler<GetOrderQuery, GetOrderResponse>
    {
        private readonly IOrderRepository _repo;
        private readonly IOrderReadRepository _repository;

        public GetOrderHandler(IOrderRepository repo, IOrderReadRepository repository)
        {
            _repo = repo;
            _repository = repository;
        }

        public async Task<GetOrderResponse> Handle(GetOrderQuery request,CancellationToken ct)
        {
            var order = await _repository.Get(request.OrderId);

            if (order is null)
            {
                throw new DomainException("Order Not Found");
            }

            return new GetOrderResponse(
                    order.Id,
                    order.Total,
                    order.Status,
                    order.Items
                        .Select(
                            x =>
                                new OrderItemResponse(
                                    x.ProductName,
                                    x.Quantity,
                                    x.Price
                                )
                        )
                        .ToList());
        }
    }
}

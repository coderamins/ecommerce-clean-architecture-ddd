using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Orders.Repositories;

namespace Ecommerce.Application.Queries.GetOrder
{
    public class GetOrderHandler
    {
        private readonly IOrderRepository _repo;
        private readonly IOrderReadRepository _repository;

        public GetOrderHandler(IOrderRepository repo, IOrderReadRepository repository)
        {
            _repo = repo;
            _repository = repository;
        }

        public async Task<GetOrderResponse> Execute(GetOrderQuery query)
        {
            var order = await _repository.Get(query.OrderId);

            if (order is null)
            {
                throw new DomainException("Order Not Found");
            }

            return new(
                    order.Id,
                    order.Total,
                    order.IsPaid,
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

using Ecommerce.Domain.Common;
using Ecommerce.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Queries.GetOrder
{
    public class GetOrderHandler
    {
        private readonly IOrderRepository _repo;

        public GetOrderHandler(IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<GetOrderResponse> Execute(GetOrderQuery query)
        {
            var order = await _repo.Get(query.OrderId);

            if(order is null)
            {
                throw new DomainException("Order Not Found");
            }

            return new(
                    order.Id,
                    order.Total.Amount,
                    order.IsPaid,
                    order.Items
                        .Select(
                            x => new OrderItemResponse(
                                     x.ProductName,
                            x.Quantity,
                            x.Price.Amount

                                )).ToList()
                );
        }
    }
}

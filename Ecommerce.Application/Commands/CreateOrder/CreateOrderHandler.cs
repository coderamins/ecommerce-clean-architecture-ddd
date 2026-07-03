using Ecommerce.Domain.Common;
using Ecommerce.Domain.Orders;
using Ecommerce.Domain.Orders.Repositories;
using Ecommerce.Domain.Orders.ValueObjects;

namespace Ecommerce.Application.Commands.CreateOrder
{
    public class CreateOrderHandler
    {
        private readonly IOrderRepository _repository;

        public CreateOrderHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Execute(
            CreateOrderCommand cmd)
        {
            var order = Order.Create();

            foreach (var item in cmd.Order.Items)
            {
                var result = order.AddItem(
                    item.ProductName,
                    item.Quantity,
                    new Money(item.price));

                if (!result.IsSuccess)
                {
                    throw new DomainException(result.Error!);
                }
            }

            await _repository.Save(order);

            return order.Id;
        }
    }
}

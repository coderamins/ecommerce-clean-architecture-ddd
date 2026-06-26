using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Factories;
using Ecommerce.Domain.Repositories;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Application.Commands.CreateOrder
{
    public class CreateOrderHandler
    {
        private readonly IOrderRepository _repository;
        //private readonly IOrderFactory _orderFactory;

        public CreateOrderHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Execute(
            CreateOrderCommand cmd)
        {
            var order = Order.Create();
                //_orderFactory
                //.Create(customerId,items);

            foreach(var item in cmd.Order.Items)
            {
                order.AddItem(
                    item.ProductName,
                    item.Quantity,
                    new Money(item.price));
            }

            await _repository.Save(order);

            return order.Id;
        }
    }
}

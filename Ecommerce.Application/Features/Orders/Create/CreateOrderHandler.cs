using Ecommerce.Domain.Common;
using Ecommerce.Domain.Orders;
using Ecommerce.Domain.Orders.Repositories;
using Ecommerce.Domain.Orders.ValueObjects;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Create
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _repository;

        public CreateOrderHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateOrderCommand cmd,CancellationToken ct)
        {
            var order = Order.Create();

            foreach (var item in cmd.Items)
            {
                var result = order.AddItem(
                    item.ProductName,
                    item.Quantity,
                    new Money(item.Price));

                if (!result.IsSuccess)
                    throw new DomainException(result.Error!);
            }

            order.CompleteCreation();

            await _repository.Save(order);

            return order.Id;
        }
    }
}

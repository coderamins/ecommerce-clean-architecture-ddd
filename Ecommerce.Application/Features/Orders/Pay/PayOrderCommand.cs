using Ecommerce.Application.Common.Abstractions;
using MediatR;
using System.Windows.Input;

namespace Ecommerce.Application.Features.Orders.Pay
{
    public sealed record PayOrderCommand(Guid OrderId): ICommand<Unit>;
}

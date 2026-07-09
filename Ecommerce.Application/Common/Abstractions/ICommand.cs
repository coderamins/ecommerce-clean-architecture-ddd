using MediatR;

namespace Ecommerce.Application.Common.Abstractions
{
    public interface ICommand<out TResponse>:IRequest<TResponse>
    {
    }
}

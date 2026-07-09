using MediatR;

namespace Ecommerce.Application.Common.Abstractions
{
    public interface IQuery<out TResponse>:IRequest<TResponse>
    {
    }
}

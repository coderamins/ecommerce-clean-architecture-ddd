using Ecommerce.Application.Queries.GetOrder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Interfaces
{
    public interface IOrderReadRepository
    {
        Task<GetOrderResponse?> Get(Guid id);
    }
}

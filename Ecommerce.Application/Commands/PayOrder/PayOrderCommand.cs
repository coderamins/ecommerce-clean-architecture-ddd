using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Commands.PayOrder
{
    public record PayOrderCommand(Guid orderId);
}

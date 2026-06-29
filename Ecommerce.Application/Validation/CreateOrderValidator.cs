using Ecommerce.Application.Commands.CreateOrder;
using Ecommerce.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Validation
{
    public class CreateOrderValidator:AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.Items).NotEmpty();

            RuleForEach(x => x.Items)
                .ChildRules(item =>
                {
                    item.RuleFor(x => x.Quantity).GreaterThan(0);
                    item.RuleFor(x => x.price).GreaterThan(0);
                });
        }

    }
}

using Ecommerce.Application.Features.Orders.Create;
using FluentValidation;

namespace Ecommerce.Application.Validation
{
    public class CreateOrderValidator:AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("Order must have at least one item.");

            RuleForEach(x => x.Items)
                .ChildRules(item =>
                {
                    item.RuleFor(x => x.ProductName)
                        .NotEmpty()
                        .MaximumLength(100);

                    item.RuleFor(x => x.Quantity)
                        .GreaterThan(0);

                    item.RuleFor(x => x.Price)
                        .GreaterThan(0);
                });
        }

    }
}

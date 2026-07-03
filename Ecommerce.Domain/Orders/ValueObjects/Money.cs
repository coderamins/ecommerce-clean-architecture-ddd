namespace Ecommerce.Domain.Orders.ValueObjects
{
    public record Money(decimal Amount)
    {
        public static Money Zero => new(0);

        public static Money operator +(Money a, Money b)
            => new(a.Amount + b.Amount);
    }
}

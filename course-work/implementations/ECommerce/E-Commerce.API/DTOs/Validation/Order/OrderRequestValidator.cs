using E_Commerce.API.DTOs.Order;
using FluentValidation;

namespace E_Commerce.API.DTOs.Validation.Order
{
    public class OrderRequestValidator : AbstractValidator<OrderRequestDto>
    {
        public OrderRequestValidator()
        {
            RuleFor(x => x.Address).NotEmpty().MaximumLength(80).WithMessage("Address must be present");
        }
    }
}

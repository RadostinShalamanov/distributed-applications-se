using E_Commerce.FrontEnd.Models.Orders;
using FluentValidation;

namespace E_Commerce.API.DTOs.Validation.Order
{
    public class OrderRequestValidator : AbstractValidator<OrderRequestModel>
    {
        public OrderRequestValidator()
        {
            RuleFor(x => x.Address).NotEmpty().MaximumLength(80).WithMessage("Address must be present");
        }
    }
}

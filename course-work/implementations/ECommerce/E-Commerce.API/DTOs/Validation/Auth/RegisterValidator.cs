using E_Commerce.API.DTOs.Auth;
using E_Commerce.API.DTOs.Product;
using E_Commerce.API.Helpers;
using FluentValidation;

namespace E_Commerce.API.DTOs.Validation.Auth
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().NotEmpty().WithMessage("Email is invalid");
            RuleFor(x => x.Username).NotEmpty().MaximumLength(20).WithMessage("Username is invalid");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(15).WithMessage("Password is invalid"); 
        }
    }
}

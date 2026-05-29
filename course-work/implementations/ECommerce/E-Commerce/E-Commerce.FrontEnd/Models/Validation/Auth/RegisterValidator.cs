using E_Commerce.FrontEnd.Models.Auth;
using FluentValidation;

namespace E_Commerce.API.DTOs.Validation.Auth
{
    public class RegisterValidator : AbstractValidator<RegisterModel>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().NotEmpty().WithMessage("Email is invalid");
            RuleFor(x => x.Username).NotEmpty().MaximumLength(20).WithMessage("Username is invalid");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(15).WithMessage("Password is invalid"); 
        }
    }
}

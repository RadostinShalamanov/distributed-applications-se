using E_Commerce.FrontEnd.Models.Users;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.Validation.User
{
    public class UserRequestValidator : AbstractValidator<UserRequestModel>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty().MaximumLength(30).WithMessage("Username is not valid");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(50).WithMessage("Invalid email");
            RuleFor(x => x.PasswordHash).NotEmpty().MaximumLength(50).WithMessage("Password up to 20 characters");
            RuleFor(x => x.Role).NotEmpty().MaximumLength(20).WithMessage("Role is invalid");
        }

    }
}

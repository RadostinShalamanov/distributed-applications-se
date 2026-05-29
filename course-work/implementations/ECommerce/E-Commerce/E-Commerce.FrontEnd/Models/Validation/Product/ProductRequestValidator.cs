using E_Commerce.FrontEnd.Models.Products;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.Validation.Product
{
    public class ProductRequestValidator : AbstractValidator<ProductRequestModel>
    {
        public ProductRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).WithMessage("String cannot be longer than 50 characters");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Value cannot be empty");
            RuleFor(x => x.Description).MaximumLength(200).WithMessage("String cannot be longer than 200 characters");
            RuleFor(x => x.CategoryId).NotEmpty();
        }
    }






}

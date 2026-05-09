using E_Commerce.API.DTOs.Product;
using E_Commerce.Data.Data;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.Validation.Product
{
    public class ProductRequestValidator : AbstractValidator<ProductRequestDto>
    {
        public ProductRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).WithMessage("String cannot be longer than 50 characters");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Value cannot be empty");
            RuleFor(x => x.Description).MaximumLength(200).WithMessage("String cannot be longer than 200 characters");
            RuleFor(x => x.CategoryId).NotEmpty();
        }
    }



    //    [Required]
    //    [MaxLength(50)]

    //    public string Name { get; set; }



    //    [Required]
    //    public decimal Price { get; set; }



}

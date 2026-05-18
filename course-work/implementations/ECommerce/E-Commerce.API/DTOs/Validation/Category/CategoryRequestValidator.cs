using E_Commerce.API.DTOs.Category;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.Validation.Category
{
    public class CategoryRequestValidator : AbstractValidator<CategoryRequestDto>
    {
        public CategoryRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).WithMessage("Invalid category name ");

            RuleFor(x => x.Description).NotEmpty().MaximumLength(100).WithMessage("Invalid description ");
        }
    }

    
}

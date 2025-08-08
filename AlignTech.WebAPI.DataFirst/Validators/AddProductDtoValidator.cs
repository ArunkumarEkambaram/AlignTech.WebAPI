using AlignTech.WebAPI.DataFirst.DTOs;
using FluentValidation;

namespace AlignTech.WebAPI.DataFirst.Validators
{
    public class AddProductDtoValidator : AbstractValidator<AddProductDto>
    {
        public AddProductDtoValidator()
        {
            RuleFor(p => p.ProductId)
                .NotEmpty().WithMessage("Product Id cannot be empty")
                .Must(p => p.StartsWith("P")).WithMessage("Product Id must starts with 'P'");

            RuleFor(p => p.ProductName)
                .NotEmpty().WithMessage("Product Name cannot be blank")
                .MinimumLength(2).WithMessage("Product Name must be atleast 2 characters")
                .MaximumLength(100).WithMessage("Product Name cannot exceed 100 characters");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than or equal to 1");

            RuleFor(p => p.QuantityAvailable)
                .InclusiveBetween(1, 10)
                .WithMessage("Quantity should be between 1 and 10");

            RuleFor(p => p.CategoryId)
               .Must(id => !id.HasValue || (id.Value > 0 && id.Value <= 255))
               .WithMessage("Category Id must be between 1 and 255");
        }
    }
}

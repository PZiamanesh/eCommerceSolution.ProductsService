using FluentValidation;
using ProductsMicroService.Core.Dtos;

namespace ProductsMicroService.Core.Validators;

public class ProductAddRequestValidator : AbstractValidator<ProductAddRequest>
{
    public ProductAddRequestValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("ProductName is required");

        RuleFor(x => x.Category)
            .IsInEnum().WithMessage("category does not provide selected option");

        RuleFor(x => x.UnitPrice)
            .InclusiveBetween(0, double.MaxValue).WithMessage("UnitPrice cannot be negative number");

        RuleFor(x => x.QuantityInStock)
            .InclusiveBetween(0, int.MaxValue).WithMessage("QuantityInStock cannot be negative number");
    }
}

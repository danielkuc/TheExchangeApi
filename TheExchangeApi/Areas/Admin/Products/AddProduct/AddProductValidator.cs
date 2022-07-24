using FluentValidation;

namespace TheExchangeApi.Areas.Admin.Products.AddProduct
{
    public class AddProductValidator : AbstractValidator<AddProduct.Request>
    {
        public AddProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .Length(3,20)
                .WithMessage("Product name should be between 3 and 20 characters");
            RuleFor(p => p.Description)
                .NotEmpty()
                .Length(5, 25)
                .WithMessage("Product description should be between 5 and 25 characters");
            RuleFor(p => p.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
            RuleFor(p => p.IsActive)
                .NotEmpty();
            RuleFor(p => p.Quantity)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
        }
    }
}

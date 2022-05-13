using FluentValidation;

namespace TheExchangeApi.Areas.Admin.Products.AddProduct
{
    public class AddProductValidator : AbstractValidator<AddProduct.Request>
    {
        public AddProductValidator()
        {
            RuleFor(c => c.PassedProduct.Name)
                .NotEmpty()
                .Length(3,20)
                .WithMessage("Product name should be between 3 and 20 characters");
            RuleFor(c => c.PassedProduct.Description)
                .NotEmpty()
                .Length(5, 25)
                .WithMessage("Product description should be between 5 and 25 characters");
            RuleFor(c => c.PassedProduct.Price)
                .NotEmpty(); 
        }
    }
}

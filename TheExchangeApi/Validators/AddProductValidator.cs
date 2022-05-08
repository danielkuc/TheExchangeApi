using FluentValidation;
using TheExchangeApi.Areas.Admin.Products.AddProduct;

namespace TheExchangeApi.Validators
{
    public class AddProductValidator : AbstractValidator<AddProduct.AddProductCommand>
    {
        public AddProductValidator()
        {
            RuleFor(c => c.PassedProduct.Name).NotEmpty();
            RuleFor(c => c.PassedProduct.Description).NotEmpty();
            RuleFor(c => c.PassedProduct.Price).NotEmpty(); 
        }
    }
}

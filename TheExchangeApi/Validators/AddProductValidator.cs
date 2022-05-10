using FluentValidation;
using TheExchangeApi.Areas.Admin.Products.AddProduct;

namespace TheExchangeApi.Validators
{
    public class AddProductValidator : AbstractValidator<AddProduct.AddProductCommand>
    {
        public AddProductValidator()
        {
            RuleFor(c => c.PassedProduct.Name).NotEmpty().Length(3,20);
            RuleFor(c => c.PassedProduct.Description).NotEmpty().Length(5, 25);
            RuleFor(c => c.PassedProduct.Price).NotEmpty(); 
        }
    }
}

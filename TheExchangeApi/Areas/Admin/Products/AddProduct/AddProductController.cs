using MediatR;
using Microsoft.AspNetCore.Mvc;
using static TheExchangeApi.Areas.Admin.Products.ActivateProduct.ActivateProduct;

namespace TheExchangeApi.Areas.Admin.Products.AddProduct
{
    [Route("admin/product.add")]
    public class AddProductController : AccessController
    {

        [HttpPost]
        //public async Task<IActionResult> CreateNewProduct(Product newProduct)
        //{
        //    var createdProduct = await _mediator.Send(new AddProduct.AddProductCommand(newProduct));

        //    return Created("admin/product.add", createdProduct);
        //}

        public async Task<Response> Action(
            [FromBody] Request request,
            [FromServices] ISender mediator,
            CancellationToken cancellationToken) => await mediator.Send(request, cancellationToken);
    }
}

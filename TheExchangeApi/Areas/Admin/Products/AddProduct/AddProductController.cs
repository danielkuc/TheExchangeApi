﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.AddProduct
{
    [ApiController]
    [Route("admin/product.add")]
    [EnableCors("myFrontendPolicy")]
    public class AddProductController : ControllerBase
    {
        private readonly ISender _mediator;

        public AddProductController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = "WriteAccess")]
        public async Task<IActionResult> CreateNewProduct(Product newProduct)
        {
            var createdProduct = await _mediator.Send(new AddProduct.AddProductCommand(
                newProduct.Name,
                newProduct.Description,
                newProduct.Price,
                newProduct.IsAvailable,
                newProduct.Quantity,
                newProduct.AddedBy
                ));

            return Created("admin/product.add", createdProduct);
        }
    }
}

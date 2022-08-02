using MongoDB.Driver;
using TheExchangeApi.Models;
using MediatR;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TheExchangeApi.Models.Cart;

namespace TheExchangeApi.Areas.Shop.Cart
{
    public class CartController : BaseShopController
    {
        private readonly IMongoCollection<Product> _collection;
        private readonly IMediator _mediator;

        public CartController(IMongoCollection<Product> collection, IMediator mediator)
        {
            _collection = collection;
            _mediator = mediator;
        }

        [Route("admin/cart.item.add")]
        [HttpGet]
        public async Task<ShoppingCart> AddItem(ObjectId id)
        {
            var product = _collection.AsQueryable().First(p => p.Id == id);
            var shoppingCart = GetCart();

            shoppingCart.IncrementQuantity(product);

            HttpContext.Session.SetString("Cart", shoppingCart.ToString());
            
            return await Task.FromResult(shoppingCart);
        }

        [Route("admin/cart.checkout")]
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var cart = GetCart();
            var request = new Checkout.Request { Cart = cart };
            var response = await _mediator.Send(request);
            HttpContext.Session.SetString("Cart", cart.ToString());
            return RedirectToPage("/Orders/Show", new { id = response.OrderId });
        }

        private ShoppingCart GetCart()
        {
            return new ShoppingCart();
        }
    }
}

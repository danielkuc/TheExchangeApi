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

        public async Task<IActionResult> AddItem(ObjectId id, ShoppingCart cart)
        {
            var product = _collection.AsQueryable().First(p => p.Id == id);
            var shoppingCart = GetCart();
            cart.IncrementQuantity(product);
            HttpContext.Session.SetString("Cart", shoppingCart.ToString());
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private ShoppingCart GetCart()
        {
            return new ShoppingCart();
        }
    }
}

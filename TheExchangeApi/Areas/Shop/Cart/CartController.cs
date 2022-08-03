using MongoDB.Driver;
using TheExchangeApi.Models;
using MediatR;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using TheExchangeApi.Models.Cart;

namespace TheExchangeApi.Areas.Shop.Cart
{
    public class CartController : BaseShopController
    {
        private readonly IMongoCollection<Product> _collection;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartController(IMongoCollection<Product> collection, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _collection = collection;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
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

        private byte[] GetCartId()
        {
            if(!_httpContextAccessor.HttpContext.Session.TryGetValue("CartId", out var cart))
            {
                _httpContextAccessor.HttpContext.Session.Set("CartId", new Guid().ToByteArray());
            };
            return cart;
        }
            

    }
}

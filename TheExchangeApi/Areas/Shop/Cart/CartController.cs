using MongoDB.Driver;
using TheExchangeApi.Models;
using MediatR;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;

namespace TheExchangeApi.Areas.Shop.Cart
{
    public class CartController : BaseShopController
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<ShoppingCart> _cartCollection;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartController(IMongoCollection<Product> productCollection
            , IMongoCollection<ShoppingCart> cartCollection
            , IMediator mediator
            , IHttpContextAccessor httpContextAccessor)
        {
            _productCollection = productCollection;
            _cartCollection = cartCollection;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [Route("admin/cart.item.old")]
        [HttpPost]
        public async Task<ShoppingCart> AddItem(string id)
        {
            var product = _productCollection.AsQueryable().First(p => p.Id == id);
            //var newCartId = GetCartId();
            var newCartId = Guid.NewGuid().ToByteArray();
            if(_cartCollection.AsQueryable().Count(sc => sc.CartId == newCartId) > 0)
            {
                var existingCart = _cartCollection.AsQueryable().Where(sc => sc.CartId == newCartId).Single();
                existingCart.IncrementQuantity(product);
            };

            var newShoppingCart = new ShoppingCart(newCartId);

            newShoppingCart.IncrementQuantity(product);
            await _cartCollection.InsertOneAsync(newShoppingCart);
            
            return await Task.FromResult(newShoppingCart);
        }

        //[Route("admin/cart.checkout")]
        //[HttpGet]
        //public async Task<IActionResult> Checkout()
        //{
            //var cart = GetCartId();
            //var request = new Checkout.Request { Cart = cart };
            //var response = await _mediator.Send(request);
            //HttpContext.Session.SetString("Cart", cart.ToString());
            //return RedirectToPage("/Orders/Show", new { id = response.OrderId });
        //}

        private byte[] GetCartId()
        {
            var cartId = new byte[] {};

            if (!_httpContextAccessor.HttpContext.Session.TryGetValue("CartId", out cartId))
            {
                var newId = Guid.NewGuid().ToByteArray();
                _httpContextAccessor.HttpContext.Session.Set("CartId", newId);
                cartId = newId;
            }
            return cartId;
        }
           

    }
}

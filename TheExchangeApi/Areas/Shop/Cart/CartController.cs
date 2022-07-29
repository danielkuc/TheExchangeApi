using MongoDB.Driver;
using TheExchangeApi.Models;
using MediatR;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> AddItem(ObjectId id)
        {
            var product = _collection.AsQueryable().First(p => p.Id == id);
            throw new NotImplementedException();
        }
    }
}

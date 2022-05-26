using MediatR;
using MongoDB.Driver;
using MongoDB.Bson;
using TheExchangeApi.Models;
using System.Text.RegularExpressions;

namespace TheExchangeApi.Areas.Admin.Products.FindManyProducts
{
    public class FindManyProducts
    {
        public record Request(string? Name, double? PriceFrom, double? PriceTo) : IRequest<Response>;
        public record Response(List<Product> ProductList);

        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly IMongoCollection<Product> _collection;

            public RequestHandler(IMongoCollection<Product> collection)
            {
                _collection = collection;
            }

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                //var filter = Builders<Product>.Filter.Eq(product => product.IsAvailable, true);
                var query = _collection.AsQueryable().Where(p => p.IsAvailable);

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    //var nameFilter = Builders<Product>.Filter.
                    //    Regex("name", new BsonRegularExpression(new Regex(request.Name, RegexOptions.IgnoreCase)));
                    //filter &= nameFilter;
                    query = query.Where(p => p.Name.Contains(request.Name));
                }

                if (request.PriceFrom.HasValue)
                {
                    //var convertedPriceFrom = Convert.ToDouble(request.PriceFrom);
                    //var convertedPriceTo = Convert.ToDouble(request.PriceTo);
                    //var priceFilter = Builders<Product>.Filter.Gt("price",convertedPriceFrom) & Builders<Product>.Filter.Lt("price", convertedPriceTo);
                    //filter &= priceFilter;
                    query = query.Where(p => p.Price >= request.PriceFrom);
                }

                if (request.PriceTo.HasValue)
                {
                    query = query.Where(p => p.Price <= request.PriceTo);
                }
                //var products = _collection.Find(filter).ToList(cancellationToken: cancellationToken);

                return Task.FromResult(new Response(query.ToList()));
            }
        }
    }
}

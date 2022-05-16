using MediatR;
using MongoDB.Driver;
using MongoDB.Bson;
using TheExchangeApi.Models;
using System.Text.RegularExpressions;

namespace TheExchangeApi.Areas.Admin.Products.FindManyProducts
{
    public class FindManyProducts
    {
        public record Request(string? Name, string? PriceFrom, string? PriceTo) : IRequest<Result>;
        public record Result(List<Product> ProductList);

        public class RequestHandler : IRequestHandler<Request, Result>
        {
            private readonly IMongoCollection<Product> _collection;

            public RequestHandler(IMongoCollection<Product> collection)
            {
                _collection = collection;
            }

            public Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                var filter = Builders<Product>.Filter.Eq(product => product.IsAvailable, true);

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    var nameFilter = Builders<Product>.Filter.
                        Regex("name", new BsonRegularExpression(new Regex(request.Name, RegexOptions.IgnoreCase)));
                    filter &= nameFilter;
                }

                if (!string.IsNullOrWhiteSpace(request.PriceFrom) && !string.IsNullOrWhiteSpace(request.PriceTo))
                {
                    var convertedPriceFrom = Convert.ToDouble(request.PriceFrom);
                    var convertedPriceTo = Convert.ToDouble(request.PriceTo);
                    var priceFilter = Builders<Product>.Filter.Gt("price",convertedPriceFrom) & Builders<Product>.Filter.Lt("price", convertedPriceTo);
                    filter &= priceFilter;
                }
                var products = _collection.Find(filter).ToList(cancellationToken: cancellationToken);

                return Task.FromResult(new Result(products));
            }
        }
    }
}

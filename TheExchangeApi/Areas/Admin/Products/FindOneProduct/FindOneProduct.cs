using MongoDB.Driver;
using MediatR;
using TheExchangeApi.Models;
using MongoDB.Bson;

namespace TheExchangeApi.Areas.Admin.Products.FindOneProduct
{
    public class FindOneProduct
    {
        public record ProductRequest(Product Product) : IRequest<Response>;
        public record Response(Product Product);

        public class RequestHandler : IRequestHandler<ProductRequest, Response>
        {
            private readonly IMongoCollection<Product> _collection;

            public RequestHandler(IMongoCollection<Product> collection)
            {
                _collection = collection;
            }

            public async Task<Response> Handle(ProductRequest request, CancellationToken cancellationToken)
            {
                var firstFoundProduct = _collection.AsQueryable()
                    .First(p => p.Id == request.Product.Id);

                return await Task.FromResult(new Response(firstFoundProduct));
            }
        }

    }
}

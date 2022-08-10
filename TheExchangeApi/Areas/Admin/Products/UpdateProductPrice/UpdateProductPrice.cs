using MediatR;
using MongoDB.Driver;
using TheExchangeApi.Models;
using Polly;

namespace TheExchangeApi.Areas.Admin.Products.UpdateProductPrice
{
    public class UpdateProductPrice
    {
        public record Request(Product ProductToUpdate) :IRequest<Response>;
        public record Response;
        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly IMongoCollection<Product> _collection;

            public RequestHandler(IMongoCollection<Product> collection)
            {
                _collection = collection;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dbProduct = _collection.Find(p => p.Id == request.ProductToUpdate.Id).SingleAsync();
                var retryPolicy = Policy.Handle<Exception>().Retry(retryCount: 3);

                if (request.ProductToUpdate.Version != dbProduct.Result.Version)
                {
                    throw new Exception($"Failed to update document. Database version='{dbProduct.Result.Version}' Current version='{request.ProductToUpdate.Version}'");
                }

                var NewVersion = Guid.NewGuid();

                _collection.UpdateOne(p => p.Id == request.ProductToUpdate.Id, Builders<Product>
                                            .Update
                                            .Set(p => p.Price, request.ProductToUpdate.Price)
                                            .Set(p => p.Version, NewVersion));

                return await Task.FromResult(new Response());
            }
        }
    }
}

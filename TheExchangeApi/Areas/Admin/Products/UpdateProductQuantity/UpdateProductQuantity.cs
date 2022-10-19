using MediatR;
using MongoDB.Driver;
using TheExchangeApi.Models;
using Polly;

namespace TheExchangeApi.Areas.Admin.Products.UpdateProductQuantity
{
    public class UpdateProductQuantity
    {
        public record Request(Product ProductToUpdate) : IRequest<Response>;
        public record Response();
        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly IMongoCollection<Product> _collection;

            public RequestHandler(IMongoCollection<Product> collection)
            {
                _collection = collection;
            }
            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dbProduct = _collection.Find(p => p.Id == request.ProductToUpdate.Id).SingleAsync(cancellationToken: cancellationToken);
                var retryPolicy = Policy.Handle<Exception>().Retry(retryCount: 3);

                if (request.ProductToUpdate.Version != dbProduct.Result.Version)
                {
                    throw new Exception($"Failed to update document. Database version='{dbProduct.Result.Version}' Current version='{request.ProductToUpdate.Version}'");
                }

                var NewVersion = Guid.NewGuid();

                _collection.UpdateOne(p => p.Id == request.ProductToUpdate.Id, Builders<Product>
                                            .Update
                                            .Set(p => p.Quantity, request.ProductToUpdate.Quantity)
                                            .Set(p => p.Version, NewVersion), cancellationToken: cancellationToken);
                return Task.FromResult(new Response());
            }
        }
    }
}

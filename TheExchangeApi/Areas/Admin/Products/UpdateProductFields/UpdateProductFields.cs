using MediatR;
using MongoDB.Driver;
using TheExchangeApi.Models;
using Polly;

namespace TheExchangeApi.Areas.Admin.Products.UpdateProductFields
{
    public class UpdateProductFields
    {
        public record Request (Product ProductToUpdate) : IRequest<Response>;
        public record Response;

        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly IMongoCollection<Product> _collection;

            public RequestHandler(IMongoCollection<Product> collection)
            {
                _collection = collection;
            }

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dbProduct = _collection.AsQueryable().Where(p => p.Id == request.ProductToUpdate.Id).Single();
                var newProduct = request.ProductToUpdate;

                var retryPolicy = Policy.Handle<Exception>().WaitAndRetryAsync(retryCount: 3, sleepDurationProvider: _ => TimeSpan.FromSeconds(2));

                if (dbProduct.Version != newProduct.Version)
                {
                    throw new Exception($"Failed to update document. Id='{dbProduct.Version}' ConcurrencyId='{request.ProductToUpdate.Version}'");
                }
                newProduct.Version++;
                _collection.ReplaceOneAsync(p => p.Id == newProduct.Id, newProduct, new ReplaceOptions { IsUpsert = false}, cancellationToken: cancellationToken);
                return Task.FromResult(new Response());
            }
        }
    }
}

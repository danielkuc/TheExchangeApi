using MediatR;
using MongoDB.Driver;
using TheExchangeApi.Models;
using Polly;

namespace TheExchangeApi.Areas.Admin.Products.UpdateProduct
{
    public class UpdateProduct
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

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dbProduct = _collection.AsQueryable()
                    .Where(p => p.Id == request.ProductToUpdate.Id)
                    .Single();

                var newProduct = request.ProductToUpdate;

                var retryPolicy = Policy.Handle<Exception>().Retry(retryCount: 3);

                if (newProduct.Version != dbProduct.Version)
                {
                    throw new Exception($"Failed to update document. Database version='{dbProduct.Version}' Current version='{request.ProductToUpdate.Version}'");
                }
                newProduct.Version = ++dbProduct.Version;
                _collection
                    .ReplaceOne(p => p.Id == newProduct.Id,
                                newProduct,
                                new ReplaceOptions { IsUpsert = false},
                                cancellationToken: cancellationToken);
                
                return await Task.FromResult(new Response());
            }
        }
    }
}

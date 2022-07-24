using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.DeactivateProduct
{
    public class DeactivateProduct
    {
        public record Request(ObjectId Id) : IRequest<Response>;
        public record Response();

        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly IMongoCollection<Product> _collection;

            public RequestHandler(IMongoCollection<Product> collection)
            {
                _collection = collection;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                await _collection.FindOneAndUpdateAsync(
                    p => p.Id == request.Id,
                    Builders<Product>.Update.Set(p => p.IsAvailable, false),
                    cancellationToken: cancellationToken
                    );
                return await Task.FromResult(new Response());
            }
        }
    }
}

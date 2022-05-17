using MediatR;
using MongoDB.Driver;
using TheExchangeApi.Models;

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
                var filterById = Builders<Product>.Filter.Eq(product => product.Id, request.ProductToUpdate.Id);
                var updateProduct = Builders<Product>
                    .Update.Set(p => p.Quantity, request.ProductToUpdate.Quantity)
                                                            .Set(p => p.Price, request.ProductToUpdate.Price)
                                                            .Set(p => p.Name, request.ProductToUpdate.Name)
                                                            .Set(p => p.Description, request.ProductToUpdate.Description);

                var updatedProduct = _collection
                    .UpdateManyAsync(filterById, updateProduct, cancellationToken: cancellationToken);

                return Task.FromResult(new Response());
            }
        }
    }
}

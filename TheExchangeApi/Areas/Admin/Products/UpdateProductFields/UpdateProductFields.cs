using MediatR;
using MongoDB.Driver;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.UpdateProductFields
{
    public class UpdateProductFields
    {
        public record UpdateProductFieldsCommand (Product ProductToUpdate) : IRequest<UpdateResult>;

        public class UpdateProductFieldsHandler : IRequestHandler<UpdateProductFieldsCommand, UpdateResult>
        {
            private readonly IProductDatabaseSettings _settings;
            private readonly IMongoClient _client;

            public UpdateProductFieldsHandler(IProductDatabaseSettings settings, IMongoClient client)
            {
                _settings = settings;
                _client = client;
            }

            public Task<UpdateResult> Handle(UpdateProductFieldsCommand request, CancellationToken cancellationToken)
            {
                var productDatabase = _client.GetDatabase(_settings.DatabaseName);
                var filterById = Builders<Product>.Filter.Eq(product => product.Id, request.ProductToUpdate.Id);
                var updateProduct = Builders<Product>.Update.Set(p => p.Quantity, request.ProductToUpdate.Quantity)
                                                            .Set(p => p.Price, request.ProductToUpdate.Price)
                                                            .Set(p => p.Name, request.ProductToUpdate.Name)
                                                            .Set(p => p.Description, request.ProductToUpdate.Description);

                var updatedProduct = productDatabase.GetCollection<Product>(_settings.ProductsCollectionName).UpdateManyAsync(filterById, updateProduct1);
                
                return updatedProduct;
            }
        }
    }
}

using MediatR;
using MongoDB.Driver;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.UpdateProductFields
{
    public class UpdateProductFields
    {
        public record UpdateProductFieldsCommand (string Id) : IRequest<UpdateResult>;

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
                throw new NotImplementedException();
            }
        }
    }
}

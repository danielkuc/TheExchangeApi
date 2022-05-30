﻿using MediatR;
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
                var dbProduct = _collection.AsQueryable().Where(p => p.Id == request.ProductToUpdate.Id).Single();
                var newProduct = request.ProductToUpdate;

                if (dbProduct.Version != newProduct.Version)
                {
                    throw new Exception("Invalid product version");
                }
                newProduct.Version++;
                _collection.ReplaceOneAsync(p => p.Id == newProduct.Id, newProduct, new ReplaceOptions { IsUpsert = false}, cancellationToken: cancellationToken);
                return Task.FromResult(new Response());
            }
        }
    }
}

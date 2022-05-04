﻿using MediatR;
using TheExchangeApi.Models;
using MongoDB.Driver;

namespace TheExchangeApi.Areas.Admin.Products.AddProduct
{
    public class AddProduct
    {
        public class AddProductCommand : IRequest<Product>
        {
            public string Name { get; }
            public string Description { get; }
            public double Price { get; }
            public bool IsAvailable { get; }
            public int Quantity { get; }
            public string AddedBy { get; }

            public AddProductCommand(Product newProduct)
            {
                Name = newProduct.Name;
                Description = newProduct.Description;
                Price = newProduct.Price;
                IsAvailable = newProduct.IsAvailable;
                Quantity = newProduct.Quantity;
                AddedBy = newProduct.AddedBy;
            }
        }

        public class AddProductHandler : IRequestHandler<AddProductCommand, Product>
        {
            private readonly IProductDatabaseSettings _settings;
            private readonly IMongoClient _client;

            public AddProductHandler(IProductDatabaseSettings settings, IMongoClient client)
            {
                _settings = settings;
                _client = client;
            }
            public Task<Product> Handle(AddProductCommand command, CancellationToken cancellationToken)
            {
                var newProduct = new Product() 
                              { 
                                Name = command.Name,
                                Description = command.Description,
                                Price = command.Price,
                                IsAvailable = command.IsAvailable,
                                Quantity = command.Quantity,
                                AddedBy = command.AddedBy
                              };

                var productDatabase = _client.GetDatabase(_settings.DatabaseName);

                productDatabase.GetCollection<Product>(_settings.ProductsCollectionName)
                    .InsertOne(newProduct, cancellationToken: cancellationToken);

                return Task.FromResult(newProduct);
            }
        }
    }
}

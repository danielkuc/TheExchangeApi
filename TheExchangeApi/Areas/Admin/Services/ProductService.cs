using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;
        
        public ProductService()
        {
            var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ProductsDbSettings")["ConnectionString"];
            var DbName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ProductsDbSettings")["DbName"];
            var CollectionName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ProductsDbSettings")["CollectionName"];

            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DbName);

            _products = database.GetCollection<Product>(CollectionName);
        }

        public Product Create(Product product)
        {
            _products.InsertOne(product);
            return product;
        }
    }
}

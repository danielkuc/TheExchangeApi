using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace TheExchangeApi.Areas.Services
{
    public class ProductServices
    {
        public ProductServices()
        {
            var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ProductsDbSettings")["ConnectionString"];
            var DbName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ProductsDbSettings")["DbName"];
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DbName);
        }
    }
}

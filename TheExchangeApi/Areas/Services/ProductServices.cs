using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;

namespace TheExchangeApi.Areas.Services
{
    public class ProductServices
    {
        public ProductServices()
        {
            var client = new MongoClient("mongodb+srv://admin:Testowanie1@theexchangedb.mqzo6.mongodb.net/TheExchangeDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("TheExchangeDB");
        }
    }
}

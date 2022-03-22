namespace TheExchangeApi.Areas.Services;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
{
    public class ProductService
    {
        public ProductService()
        {
            var client = new MongoClient("mongodb+srv://admin:Testowanie1@theexchangedb.mqzo6.mongodb.net/TheExchangeDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("TheExchangeDB");
        }
    }
}

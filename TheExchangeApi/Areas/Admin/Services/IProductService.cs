using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Services
{
    public interface IProductService
    {
        List<Product> Get();
        Product Get(string id);
        Product Create(Product product);
        void Update(string id, Product product);
        void Remove(string id);
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TheExchangeApi.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Products ??= new Dictionary<string, CartProduct>();
        }

        [BsonId]
        public string Id { get; set; } = Guid.NewGuid().ToString(); 
        public double Total => Products.Sum(cp => cp.Value.SubTotal);
        public Dictionary<string, CartProduct> Products { get; set; }



        public void IncrementQuantity(Product product)
        {
            if (product.Quantity++ > product.Quantity)
            {
                throw new Exception("Requested quantity surpasses stock of the item");
            }
            if (!Products.ContainsKey(product.Id))
            {
                Products[product.Id] = new CartProduct(product);
            }
            Products[product.Id].Quantity++;
        }

        public void DecrementQUantity(Product product)
        {
            if (Products[product.Id].Quantity == 0)
            {
                Products.Remove(product.Id);
            }
            Products[product.Id].Quantity--;

        }
    }
}

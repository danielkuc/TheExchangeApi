using MongoDB.Bson;

namespace TheExchangeApi.Models
{
    public class CartProduct
    {
        public CartProduct() { }
        public CartProduct(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
        }
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double SubTotal => Price * Quantity;
    }
}

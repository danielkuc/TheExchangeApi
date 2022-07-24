namespace TheExchangeApi.Models
{
    public class Cart
    {
        public Guid Id { get; set; } = new Guid();
        public List<CartProduct> Products { get; set; } = new List<CartProduct>();
    }
}

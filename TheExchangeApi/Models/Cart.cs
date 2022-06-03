namespace TheExchangeApi.Models
{
    public class Cart
    {
        public Guid CartId { get; set; } = new Guid();
        public DateTime DateCreated { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}

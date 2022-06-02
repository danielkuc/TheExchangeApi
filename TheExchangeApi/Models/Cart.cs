namespace TheExchangeApi.Models
{
    public class Cart
    {
        public string CartId { get; set; }
        public DateTime DateCreated { get; set; }
        public List<CartItem> Items { get; set; }
    }
}

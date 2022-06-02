namespace TheExchangeApi.Models
{
    public class CartItem
    {
        public string CartId { get; set; }
        public int QuantityReserved { get; set; }
        public  Product Product { get; set; }
    }
}

namespace TheExchangeApi.Models
{
    public class CartProduct
    {
        public int Quantity { get; set; }
        // change this, what if product changes? use different type.
        public  Product Product { get; set; }
    }
}

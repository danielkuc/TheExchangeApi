namespace TheExchangeApi.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Total { get; set; }
        public bool HasBeenFulfilled { get; set; }
        public List<Product> OrderDetails { get; set; }

    }
}

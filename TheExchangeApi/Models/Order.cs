namespace TheExchangeApi.Models
{
    public class Order
    {
        public Order(Customer Customer, ShoppingCart Cart)
        {
;
            this.Customer = new Customer
            {
                FirstName = Customer.FirstName,
                LastName = Customer.LastName,
                Email = Customer.Email,
                Mobile = Customer.Mobile,
                HouseNumber = Customer.HouseNumber,
                Town = Customer.Town,
                Postcode = Customer.Postcode
            };

            this.Items = Cart.Products;
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Total { get; set; }
        public Customer Customer { get; }
        public Dictionary<string, CartProduct> Items;
    }
}

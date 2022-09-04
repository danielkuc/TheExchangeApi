namespace TheExchangeApi.Models
{
    public class Order
    {
        public Order(Customer Customer)
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
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Total { get; set; }
        public Customer Customer { get; }
    }
}

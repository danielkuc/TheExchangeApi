namespace TheExchangeApi.Areas.Admin.Models
{
    public class ProductDatabaseSettings : IProductDatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string ProductsCollectionName { get; set; } = string.Empty;
    }
}

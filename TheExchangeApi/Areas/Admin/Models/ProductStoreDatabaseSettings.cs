namespace TheExchangeApi.Areas.Admin.Models
{
    public class ProductStoreDatabaseSettings : IProductStoreDatabaseSettings
    {
        public string ProductCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}

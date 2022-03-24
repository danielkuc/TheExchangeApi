namespace TheExchangeApi.Areas.Admin.Models
{
    public interface IProductStoreDatabaseSettings
    {
        string ProductCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

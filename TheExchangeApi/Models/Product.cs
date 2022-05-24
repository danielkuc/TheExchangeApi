using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TheExchangeApi.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Price { get; set; } = 0.00;

        public bool IsAvailable { get; set; }

        public int Quantity { get; set; } = 0;

        public string AddedBy { get; set; } = string.Empty;  
    }
}

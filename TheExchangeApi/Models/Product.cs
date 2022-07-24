using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TheExchangeApi.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; } 
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;
        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;
        [BsonElement("price")]
        public double Price { get; set; } = 0.00;
        [BsonElement("isAvailable")]
        public bool IsAvailable { get; set; }
        [BsonElement("quantity")]
        public int Quantity { get; set; } = 0;
        [BsonElement("addedBy")]
        public string AddedBy { get; set; } = string.Empty;
        [BsonElement("version")]
        public long Version { get; set; } = 0;
    }
}

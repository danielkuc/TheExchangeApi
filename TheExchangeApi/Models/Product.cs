using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TheExchangeApi.Models
{
    public class UpdateResult
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("price")]
        public double Price { get; set; } = 0.00;

        [BsonElement("available")]
        public bool IsAvailable { get; set; } = false;

        [BsonElement("quantity")]
        public int Quantity { get; set; } = 0;
    }
}

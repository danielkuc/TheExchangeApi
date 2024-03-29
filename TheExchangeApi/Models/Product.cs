﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TheExchangeApi.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;
        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;
        [BsonElement("price")]
        public double Price { get; set; } = 0.00;
        [BsonElement("isActive")]
        public bool IsActive { get; set; }
        [BsonElement("quantity")]
        public int Quantity { get; set; } = 0;
        [BsonElement("version")]
        public Guid Version { get; set; } = Guid.NewGuid();
        public IFormFile Image { get; set; }
    }
}

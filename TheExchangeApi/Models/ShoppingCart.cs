﻿using MongoDB.Bson;

namespace TheExchangeApi.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Products ??= new Dictionary<ObjectId, CartProduct>();
        }

        public double Total => Products.Sum(cp => cp.Value.SubTotal);

        public Dictionary<ObjectId, CartProduct> Products { get; set; }

        public void IncrementQuantity(Product product)
        {
            if (!Products.ContainsKey(product.Id))
            {
                Products[product.Id] = new CartProduct(product);
            }
            Products[product.Id].Quantity++;
        }

        public void DecrementQUantity(Product product)
        {
            if (Products[product.Id].Quantity == 0)
            {
                Products.Remove(product.Id);
            }
            Products[product.Id].Quantity--;

        }
    }
}
using MediatR;
using MongoDB.Driver;
using TheExchangeApi.Models;
using Polly;

namespace TheExchangeApi.Areas.Admin.Products.UpdateProductPrice
{
    public class UpdateProductPrice
    {
        public record Request(Product ProductToUpdate) :IRequest<Response>;
        public record Response;
    }
}

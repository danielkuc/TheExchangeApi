using MediatR;

namespace TheExchangeApi.Areas.Admin.Features.Products
{
    public class GetProductById
    {
        //Query/Command
        //All the data needed to execute
        public record ProductByIdQuery(int Id) : IRequest<RetreivedProduct>;

        public class Handler : IRequestHandler<ProductByIdQuery, RetreivedProduct>
{
            public Task<RetreivedProduct> Handle(ProductByIdQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        public record RetreivedProduct(int Id, string Name, string description, double price);
    }
}

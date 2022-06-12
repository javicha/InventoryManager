using MediatR;

namespace Inventory.Application.Features.Products.Queries.GetAllProducts
{
    /// <summary>
    /// CQRS pattern:GetAllProductsQuery query parameters
    /// </summary>
    public class GetAllProductsQuery : IRequest<List<ProductDTO>>
    {
        public GetAllProductsQuery() { }
    }
}
